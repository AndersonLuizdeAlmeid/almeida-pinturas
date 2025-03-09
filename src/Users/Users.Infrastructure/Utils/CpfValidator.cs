using System.Linq;

namespace Users.Infrastructure.Utils;
public static class CpfValidator
{
    public static bool BeValidCpf(string cpf)
    {
        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11 || !cpf.All(char.IsDigit) || cpf.Distinct().Count() == 1) return false;

        var firstDigit = cpf[9];
        var secondDigit = cpf[10];

        // Primeiro dígito verificador
        var sum1 = 0;
        var weight1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        for (int i = 0; i < 9; i++)
        {
            sum1 += int.Parse(cpf[i].ToString()) * weight1[i];
        }
        var remainder1 = sum1 % 11;
        var checkDigit1 = remainder1 < 2 ? 0 : 11 - remainder1;

        // Segundo dígito verificador
        var sum2 = 0;
        var weight2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        for (int i = 0; i < 10; i++)
        {
            sum2 += int.Parse(cpf[i].ToString()) * weight2[i];
        }
        var remainder2 = sum2 % 11;
        var checkDigit2 = remainder2 < 2 ? 0 : 11 - remainder2;

        return firstDigit.ToString() == checkDigit1.ToString() && secondDigit.ToString() == checkDigit2.ToString();
    }
}