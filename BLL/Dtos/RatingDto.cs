using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos
{
    public class RatingDto
    {
        [Range(1, 5)]
        public decimal Score { get; set; }
    }
}
