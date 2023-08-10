using System;
using System.Text.RegularExpressions;
public class Extractor
    {
    public static string ExtractName(string input)
    {
        // This regular expression matches common name patterns, but it may not cover all cases.
        string namePattern = @"\b[A-Z][a-zA-Z'-]+\b";

        Match match = Regex.Match(input, namePattern);

        if (match.Success)
        {
            return match.Value;
        }
        else
        {
            return null; // No name found in the input string.
        }
    }
    public static List<string> ExtractPhoneNumber(string input)
    {
        List<string> result = new List<string>();

        string[] phonePatterns = new string[]
        {
         @"\d{3}\s\d{3}\s\d{2}\s\d{2}",
            @"\d{3}[-.]\d{3}[-.]\d{4}",
            @"\+\d{2}\s\d{3}\s\d{3}\s\d{2}\s\d{2}",
            @"\b\d{4}\s?\d{7}\b"
        };

        foreach (string pattern in phonePatterns)
        {
            MatchCollection matches = Regex.Matches(input, pattern);
            foreach (Match match in matches)
            {
                string phoneNumber = match.Value;
                // Kontrol et, eğer bu numara zaten eklenmişse ekleme.
                if (!result.Contains(phoneNumber))
                {
                    result.Add(phoneNumber);
                    break; // İlk eşleşmeyi bulduktan sonra döngüden çık.
                }
            }
        }

        return result;
    }
    public static string ExtractSurname(string input)
    {
        string surnamePattern = @"(?<=\b[A-Z][a-zA-Z]*\s)[^\s]+";

        Match match = Regex.Match(input, surnamePattern);

        if (match.Success)
        {
            return match.Value;
        }
        else
        {
            return null; // Soyadı çıkaramadık.
        }
    }

    public static string ExtractWebsiteURL(string input)
    {
        // This regular expression matches most common website URL formats.
        // It may not cover all possible URL variations.
        string urlPattern = @"\b(?:https?://|www\.)\S+\b";

        Match match = Regex.Match(input, urlPattern);

        if (match.Success)
        {
            return match.Value;
        }
        else
        {
            return null; // No website URL found in the input string.
        }
    }
    public static List<string> ExtractEmailAddresses(string input)
    {
        List<string> result = new List<string>();
        input = input.ToLower();

        string[] emailPatterns = new string[]
        {
        @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b",
        @"\b[A-Za-z0-9]+@ornek\.com\b",
        @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}",
        @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")©(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])"
        };

        foreach (string pattern in emailPatterns)
        {

            MatchCollection matches = Regex.Matches(input, pattern);
            foreach (Match match in matches)
            {
                string emailAddress = match.Value.Replace("©", "@");
                result.Add(emailAddress);

            }
        }

        return result;
    }





    public static string ExtractAddress(string input)
    {
        string addressPattern = @"\b[A-Za-züğşıöçİĞŞÜÇ]+\s+Mah\.\s+[A-Za-züğşıöçİĞŞÜÇ]+\s+[A-Za-züğşıöçİĞŞÜÇ]+\s+No:\d+\s+\d+\s+[A-Za-züğşıöçİĞŞÜÇ]+\/[A-Za-züğşıöçİĞŞÜÇ]+\b";



        Match match = Regex.Match(input, addressPattern, RegexOptions.IgnoreCase);

        if (match.Success)
        {
            string address = match.Value.Trim();
            return address;
        }

        // Eğer adres bulunamazsa, boş bir string döndürülebilir veya null değeri kullanılabilir.
        return string.Empty;
    }

}

