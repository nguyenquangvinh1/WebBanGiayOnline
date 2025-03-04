namespace WebBanGiay.view_mode
{
    public class EmployeeListViewModel
    {
        public List<NhanVien_Model> Employees { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
