namespace MiniE_Commerce.BLL.Helper.Response
{
    public class ResponseModel<T>
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public PaginationMeta Pagination { get; set; }

        public static ResponseModel<T> Success(T data, string message = null)
        {
            return new ResponseModel<T>
            {
                Ok = true,
                Message = message ?? "Success",
                Data = data,
                Pagination = null
            };
        }


        public static ResponseModel<T> Success(T data, int pageIndex, int pageSize, int count, string message = null)
        {
            return new ResponseModel<T>
            {
                Ok = true,
                Message = message ?? "Success",
                Data = data,
                Pagination = new PaginationMeta
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = count
                }
            };
        }

        public static ResponseModel<T> Error(string message)
        {
            return new ResponseModel<T>
            {
                Ok = false,
                Message = message,
                Data = default,
                Pagination = null
            };
        }
    }

    public class PaginationMeta
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
    }
}
