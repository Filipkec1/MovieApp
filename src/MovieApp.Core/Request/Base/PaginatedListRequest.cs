namespace MovieApp.Core.Request.Base
{
    /// <summary>
    /// Defines paginated list request.
    /// </summary>
    public class PaginatedListRequest
    {
        private int page = 1;
        private int size = 10;

        public int Page 
        {
            get
            {
                return page;
            }

            set
            {
                if(value < 1) 
                {
                    return;
                }
                
                page = value;
            } 
        }

        public int Size
        {
            get
            {
                return size;
            }

            set
            {
                if(value > 0 && value < 25)
                {
                    size = value;
                }
            }
        }
    }
}