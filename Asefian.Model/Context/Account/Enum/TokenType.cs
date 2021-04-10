using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account.Enum
{
    /// <summary>
    /// نوع توکن ها
    /// </summary>
    [Table(name: "TokenType", Schema = "enum")]
    public class TokenType : BaseEnum<TokenType>
    {
        public TokenType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// ایمیل
        /// </summary>
        public readonly static TokenType Email = new TokenType(1, "Email", "ایمیل");
        /// <summary>
        /// تلفن همراه
        /// </summary>
        public readonly static TokenType Mobile = new TokenType(2, "Mobile", "تلفن همراه");

        /// <summary>
        /// دریافت تمام مقدارهای نوع توکن ها به صورت لیست
        /// </summary>
        /// <returns>لیست نوع توکن ها</returns>
        public new static IEnumerable<TokenType> GetList()
        {
            return new TokenType[] {
                Email, Mobile
            };
        }
    }
}
