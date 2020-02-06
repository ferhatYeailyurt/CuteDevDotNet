using CuteDev.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CuteDev.Users.Data.BLL
{
    public class PermissionList: PermissionListBase
    {
        [PermissionDetail(Title = "Kullanıcı ekleme.")]
        public const string UsersAdd = "UsersAdd";

        [PermissionDetail(Title = "Kullanıcı güncelleme")]
        public const string UsersUpdate = "UsersUpdate";

        [PermissionDetail(Title = "Kullanıcı silme")]
        public const string UsersDelete = "UsersDelete";

        [PermissionDetail(Title = "Kullanıcı listele")]
        public const string UsersList = "UsersList";

        //

        [PermissionDetail(Title = "Product ekleme.")]
        public const string ProductAdd = "ProductAdd";

        [PermissionDetail(Title = "Product güncelleme")]
        public const string ProductUpdate = "ProductUpdate";

        [PermissionDetail(Title = "Product silme")]
        public const string ProductDelete = "ProductDelete";

        [PermissionDetail(Title = "Product listele")]
        public const string ProductList = "ProductList";

        [PermissionDetail(Title = "Category ekleme.")]
        public const string CategoryAdd = "CategoryAdd";

        [PermissionDetail(Title = "Category güncelleme")]
        public const string CategoryUpdate = "CategoryUpdate";

        [PermissionDetail(Title = "Category silme")]
        public const string CategoryDelete = "CategoryDelete";

        [PermissionDetail(Title = "Category listele")]
        public const string CategoryList = "CategoryList";

    }
}
