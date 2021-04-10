using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asefian.Common.Security
{
    /// <summary>
    /// کلاس کمکی برای دسترسی های کاربر
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// دسترسی ادمین
        /// </summary>
        public const int Admin = 0;
        /// <summary>
        /// دسترسی به صفحه اصلی و اسلایدر
        /// </summary>
        public const int MainPage = 1;
        /// <summary>
        /// دسترسی به سفارشات و آرشیو سفارشات
        /// </summary>
        public const int Invoice = 2;
        /// <summary>
        /// دسترسی به بخض کوپن
        /// </summary>
        public const int Coupon = 3;
        /// <summary>
        /// دسترسی به سند ورود محصولات
        /// </summary>
        public const int DocumentEntry = 4;
        /// <summary>
        /// دسترسی به محصولات
        /// </summary>
        public const int Product = 5;
        /// <summary>
        /// دسترسی به  فیلتر محصولات
        /// </summary>
        public const int Filter = 6;
        /// <summary>
        /// دسترسی به اخبار
        /// </summary>
        public const int News = 7;
        /// <summary>
        /// دسترسی به بلاگ
        /// </summary>
        public const int Blog = 8;
        /// <summary>
        /// دسترسی به مقالات
        /// </summary>
        public const int Article = 9;
        /// <summary>
        /// دسترسی به تیکت های پشتیبانی
        /// </summary>
        public const int Ticket = 10;
        /// <summary>
        /// دسترسی به پیام ها
        /// </summary>
        public const int Message = 11;
        /// <summary>
        /// دسترسی به خبرنامه
        /// </summary>
        public const int NewsLetter = 12;
        /// <summary>
        /// دسترسی به سوالات متداول
        /// </summary>
        public const int Faq = 13;
        /// <summary>
        /// دسترسی به دسته بندی ها
        /// </summary>
        public const int Category = 14;
        /// <summary>
        /// دسترسی به برند ها
        /// </summary>
        public const int Brand = 15;
        /// <summary>
        /// دسترسی به شعب
        /// </summary>
        public const int Branch = 16;
        /// <summary>
        /// دسترسی به  بخش کاربران
        /// </summary>
        public const int User = 17;
        /// <summary>
        /// دسترسی به گروه های کاربری
        /// </summary>
        public const int Group = 18;
        /// <summary>
        /// دسترسی به خدمات
        /// </summary>
        public const int Service = 19;
        /// <summary>
        /// دسترسی به خدمات
        /// </summary>
        public const int Settings = 20;
        /// <summary>
        /// دسترسی به سند خروج محصولات
        /// </summary>
        public const int DocumentExit = 21;

        private static string[] permissionList =
        {
            nameof(Admin).FirstCharToLower(),
            nameof(MainPage).FirstCharToLower(),
            nameof(Invoice).FirstCharToLower(),
            nameof(Coupon).FirstCharToLower(),
            nameof(DocumentEntry).FirstCharToLower(),
            nameof(Product).FirstCharToLower(),
            nameof(Filter).FirstCharToLower(),
            nameof(News).FirstCharToLower(),
            nameof(Blog).FirstCharToLower(),
            nameof(Article).FirstCharToLower(),
            nameof(Ticket).FirstCharToLower(),
            nameof(Message).FirstCharToLower(),
            nameof(NewsLetter).FirstCharToLower(),
            nameof(Faq).FirstCharToLower(),
            nameof(Category).FirstCharToLower(),
            nameof(Brand).FirstCharToLower(),
            nameof(Branch).FirstCharToLower(),
            nameof(User).FirstCharToLower(),
            nameof(Group).FirstCharToLower(),
            nameof(Service).FirstCharToLower(),
            nameof(Settings).FirstCharToLower(),
            nameof(DocumentExit).FirstCharToLower()
        };

        /// <summary>
        /// لیست دسترسی ها
        /// </summary>
        private string permission;

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="input">دسترسی های کد شده</param>
        public Permission(int input)
        {
            permission = new string(Convert.ToString(input, 2).Reverse().ToArray());
        }

        /// <summary>
        /// بررسی وجود دسترسی برای کاربر
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(int role)
        {
            if (permission.Length > role)
            {
                return permission[role] == '1';
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// دریافت لیست دسترسی های کاربر
        /// </summary>
        /// <param name="permissionCode">لیست دسترسی های کاربر به صورت تبدیل شده به کد</param>
        /// <returns>آرایه لیست دسترسی های کاربر</returns>
        public static string[] GetPermissionList(long permissionCode)
        {
            var authorized = new List<string>();

            var permission = Convert.ToString(permissionCode, 2).Reverse().ToArray();
            for (int i = 0; i < permission.Length; i++)
            {
                if (permission[i] == '1')
                    authorized.Add(permissionList[i]);
            }

            return authorized.ToArray();
        }
        
        /// <summary>
        /// دریافت لیست دسترسی های کاربر
        /// </summary>
        /// <param name="permissionCode">لیست دسترسی های کاربر به صورت تبدیل شده به کد</param>
        /// <returns>آرایه لیست دسترسی های کاربر</returns>
        public static List<int> GetPermissionCodeList(long permissionCode)
        {
            var authorized = new List<int>();

            var permission = Convert.ToString(permissionCode, 2).Reverse().ToArray();
            for (int i = 0; i < permission.Length; i++)
            {
                if (permission[i] == '1')
                    authorized.Add(i);
            }

            return authorized;
        }

        /// <summary>
        /// دریافت کد دسترسی های کاربر از یک دسترسی 
        /// </summary>
        /// <param name="permission">دسترسی مورد نظر</param>
        /// <returns>کد دسترسی های کاربر به سیستم</returns>
        public static long GetPermissionCode(int permission)
        {
            var list = new List<int>() { permission };
            return GetPermissionCode(list);
        }

        /// <summary>
        /// دریافت کد دسترسی های کاربر از یک لیست 
        /// </summary>
        /// <param name="permissionList">لیست دسترسی های مورد نیاز برای کاربر (از لیست دسترسی ها)</param>
        /// <returns>کد دسترسی های کاربر به سیستم</returns>
        public static long GetPermissionCode(List<int> permissionList)
        {
            if (permissionList == null || permissionList.Count == 0 || permissionList.Contains(0))
            {
                return 0;
            }

            // انتخاب بزرگترین عدد که دسترسی کاربر را دارد
            var biggest = permissionList.OrderBy(x => x).LastOrDefault();
            // ساخت یک رشته به طول بزرگترین کد کاربر  به صورت 0
            var stringPermission = new string('0', biggest + 1);
            // تبدیل رشته ساخته شده به رشته ای که بتوان یکی از کاراکترهای آن را تغییر داد چون رشته معمولی اجازه تغییر نمی دهد
            StringBuilder permission = new StringBuilder(stringPermission);

            for (int i = 0; i < permissionList.Count; i++)
            {
                permission[permissionList[i]] = '1';
            }

            return Convert.ToInt64(new string(permission.ToString().Reverse().ToArray()), 2);
        }

    }
}
