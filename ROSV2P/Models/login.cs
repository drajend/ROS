using System.ComponentModel.DataAnnotations;

namespace ROSV2P.Models
{
    public class login
    {

        [Required(ErrorMessage = "Username required")] // make the field required
        [Display(Name = "Username")]  // Set the display name of the field
        public string userName { get; set; }
        [Required(ErrorMessage = "Password required")]
        [Display(Name = "Password")]
        public string passWord { get; set; }


        //public static IEnumerable ValidateLogin(string UserName, string Password,string flag)
        //{

        //    string Status = string.Empty;
        //    string User = string.Empty;
        //    try
        //    {
        //        DirectoryEntry entry = new DirectoryEntry(ldapConfig, ldapDomain + UserName.Trim(), Password.Trim());
        //        try {
        //            object obj = entry.NativeObject;
        //            Status = "valid";
        //        }
        //        catch { Status = "0"; }
        //        if (Status == "valid")
        //        {
        //            if (flag == "s")
        //                User = UserName.Trim();
        //            else
        //                User = Environment.UserName.Trim();

        //            DirectorySearcher mySearcher = new DirectorySearcher(entry);
        //            mySearcher.Filter = "(&(objectClass=user)(|(cn=" + User + ")(sAMAccountName=" + User + ")))";
        //            SearchResult result = mySearcher.FindOne();

        //            foreach (string GroupPath in result.Properties["memberOf"])
        //            {
        //                if (GroupPath.Contains(ldapGroup))
        //                {
        //                    Status = "1";
        //                    break;
        //                }
        //                Status = "2";
        //            }
        //        }
        //    }
        //    catch 
        //    {
        //        Status = "0";
        //    }
        //    return Status;
        //}

        //private string Decrypt(string cipherText)
        //{
        //    string EncryptionKey = "MAKV2SPBNI99212";
        //    byte[] cipherBytes = Convert.FromBase64String(cipherText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                cs.Close();
        //            }
        //            cipherText = Encoding.Unicode.GetString(ms.ToArray());
        //        }
        //    }
        //    return cipherText;
        //}
    }
}