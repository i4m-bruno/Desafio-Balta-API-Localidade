using Desafio_Balta_API_Localidade.ViewModels;
using DesafioLocalidade.Identity.Configuration;
using DesafioLocalidade.Identity.Services;
using DesafioLocalidade.Identity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.Text;

public class IdentityServiceTests
{
#pragma warning disable CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.

    Mock<UserManager<IdentityUser>> mockUserManager;
    Mock<SignInManager<IdentityUser>> mockSignInManager;
    Mock<IOptions<JwtOptions>> optionsMock;
    SigningCredentials signingCredentials;

    public IdentityServiceTests()
    {
        // Arrange
        mockUserManager = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);

        mockSignInManager = new Mock<SignInManager<IdentityUser>>(
            mockUserManager.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(), null, null, null, null);

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("chave-super-secreta"));
        signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        optionsMock = new Mock<IOptions<JwtOptions>>();
        optionsMock.Setup(x => x.Value).Returns(new JwtOptions("Issuer", "Audience", signingCredentials, 5000));
    }

    [Fact]
    public async Task CadastrarUsuario_SucessoEsperado()
    {
        //Arrange
        mockUserManager
            .Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        var jwtOptions = optionsMock.Object;
        var service = new IdentityService(mockSignInManager.Object, mockUserManager.Object, jwtOptions);

        var usuarioViewModel = new UsuarioViewModel("teste@email.com", "Senha@123", "Senha@123");
        usuarioViewModel.Validate();

        // Act
        var result = await service.CadastrarUsuario(usuarioViewModel);

        // Assert
        Assert.True(result.Sucesso);
        Assert.True(result.Erros.Count <= 0);
        Assert.True(usuarioViewModel.Notifications.Count == 0);
    }

    [Fact]
    public async Task CadastrarUsuario_ErroEsperado_SenhaNaoConfere()
    {
        var jwtOptions = optionsMock.Object;
        var service = new IdentityService(mockSignInManager.Object, mockUserManager.Object, jwtOptions);

        var usuarioViewModel = new UsuarioViewModel("teste@email.com", "Senha@123", "Senha@1234");
        usuarioViewModel.Validate();

        mockUserManager
            .Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "User creation failed" }));

        // Act
        var result = await service.CadastrarUsuario(usuarioViewModel);

        // Assert
        Assert.False(result.Sucesso);
        Assert.True(result.Erros.Count >= 1);
        Assert.True(usuarioViewModel.Notifications.Count > 0);
    }

    [Fact]
    public async Task CadastrarUsuario_ErroEsperado_EmailInvalido()
    {
        var jwtOptions = optionsMock.Object;
        var service = new IdentityService(mockSignInManager.Object, mockUserManager.Object, jwtOptions);

        var usuarioViewModel = new UsuarioViewModel("testeemail.com", "Senha@123", "Senha@123");
        usuarioViewModel.Validate();

        mockUserManager
            .Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "User creation failed" }));

        // Act
        var result = await service.CadastrarUsuario(usuarioViewModel);

        // Assert
        Assert.False(result.Sucesso);
        Assert.True(result.Erros.Count >= 1);
        Assert.True(usuarioViewModel.Notifications.Count > 0);
    }

    [Fact]
    public async Task Login_FalhaEsperada()
    {
        var jwtOptions = optionsMock.Object;
        var service = new IdentityService(mockSignInManager.Object, mockUserManager.Object, jwtOptions);

        var usuarioLoginViewModel = new UsuarioLoginViewModel("teste@email.com", "Senha@123");
        usuarioLoginViewModel.Validate();

        mockSignInManager
            .Setup(um => um.PasswordSignInAsync(usuarioLoginViewModel.Email, usuarioLoginViewModel.Senha, false, true))
            .ReturnsAsync(SignInResult.Failed);

        // Act
        var result = await service.Login(usuarioLoginViewModel);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Sucesso);
        Assert.True(result.Erros.Count > 0);
        Assert.True(usuarioLoginViewModel.Notifications.Count == 0);
    }

#pragma warning restore CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
}