using Desafio_Balta_API_Localidade.ViewModels;
using DesafioLocalidade.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

public class IdentityServiceTests
{
#pragma warning disable CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.

    [Fact]
    public async Task CadastrarUsuario_SucessoEsperado()
    {
        // Arrange
        var mockUserManager = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);

        var mockSignInManager = new Mock<SignInManager<IdentityUser>>(
            mockUserManager.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(), null, null, null, null);

        var service = new IdentityService(mockSignInManager.Object, mockUserManager.Object);

        var usuarioViewModel = new UsuarioViewModel("teste@email.com","Senha@123","Senha@123");

        mockUserManager
            .Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await service.CadastrarUsuario(usuarioViewModel);

        // Assert
        Assert.True(result.Sucesso);
        Assert.True(result.Erros.Count <= 0);
    }

    [Fact]
    public async Task CadastrarUsuario_ErroEsperado_SenhaNaoConfere()
    {
        // Arrange
        var mockUserManager = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);

        var mockSignInManager = new Mock<SignInManager<IdentityUser>>(
            mockUserManager.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(), null, null, null, null);

        var service = new IdentityService(mockSignInManager.Object, mockUserManager.Object);

        var usuarioViewModel = new UsuarioViewModel("teste@email.com", "Senha@123", "Senha@1234");

        mockUserManager
            .Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "User creation failed" }));

        // Act
        var result = await service.CadastrarUsuario(usuarioViewModel);

        // Assert
        Assert.False(result.Sucesso);
        Assert.True(result.Erros.Count >= 1);
    }
    
    [Fact]
    public async Task CadastrarUsuario_ErroEsperado_EmailInvalido()
    {
        // Arrange
        var mockUserManager = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);

        var mockSignInManager = new Mock<SignInManager<IdentityUser>>(
            mockUserManager.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(), null, null, null, null);

        var service = new IdentityService(mockSignInManager.Object, mockUserManager.Object);

        var usuarioViewModel = new UsuarioViewModel("testeemail.com", "Senha@123", "Senha@123");

        mockUserManager
            .Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "User creation failed" }));

        // Act
        var result = await service.CadastrarUsuario(usuarioViewModel);

        // Assert
        Assert.False(result.Sucesso);
        Assert.True(result.Erros.Count >= 1);
    }

#pragma warning restore CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
}