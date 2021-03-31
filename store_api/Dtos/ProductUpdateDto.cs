using System.ComponentModel.DataAnnotations;


namespace store_api.Dtos
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
    
        public string name { get; set; }

        public string description { get; set; }
        public bool status { get; set; }

    }
}
