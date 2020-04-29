namespace AspNetCore.Example.Domain.Entities.UserAgg
{
    public class UserPermissions
    {
        //public UserPermissions(bool read, bool edit, bool delete)
        //{
        //    Read = read;
        //    Edit = edit;
        //    Delete = delete;
        //}

        public bool Read { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
    }
}
