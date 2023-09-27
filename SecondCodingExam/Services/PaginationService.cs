using SecondCodingExam.Services.Interface;

namespace SecondCodingExam.Services
{
    public class PaginationService : IPaginationService
    {
        public int GetPageNumber(int PageNumber)
        {
            return (PageNumber - 1) * Constants.PageSize;
        }
    }
}
