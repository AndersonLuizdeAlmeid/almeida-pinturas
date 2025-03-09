using Users.Infrastructure.Utils;

namespace Users.UnitTesting.Infrastructure;
public class CpfValidatorTest
{
    [Theory]
    [InlineData("123.456.789-09", true)]   // CPF válido
    [InlineData("123.456.789-00", false)]  // CPF inválido
    [InlineData("000.000.000-00", false)]  // CPF inválido (todos zeros)
    [InlineData("111.111.111-11", false)]  // CPF inválido (sequência repetitiva)
    [InlineData("12345678909", true)]      // CPF válido sem formatação
    [InlineData("1234567890", false)]      // CPF com 10 caracteres
    [InlineData("123456789012", false)]    // CPF com 12 caracteres
    [InlineData("a23.456.789-09", false)]  // CPF com caracteres não numéricos
    [InlineData("", false)]                // CPF vazio
    public void BeValidCpf_ShouldReturnExpectedResult(string cpf, bool expectedResult)
    {
        // Act
        var result = CpfValidator.BeValidCpf(cpf);

        // Assert
        Assert.Equal(expectedResult, result);
    }
}