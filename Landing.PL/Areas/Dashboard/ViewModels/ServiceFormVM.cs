namespace Landing.PL.Areas.Dashboard.ViewModels
{
    public class ServiceFormVM // تحتوي فقط اسم ووصف Create Service بدي اعمل 
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int Id { get; set; } // Edit Action تم اضافته لما نعمل 
        public bool IsDeleted {  get; set; } // Index.cshtml في already الها , لانها موجودة  Edit تم اضافتها عشان نقدر نعمل
        


        public IFormFile Image {  get; set; } // لاني بدي اوخذ الصورة والباث تبعها وكل اشي فيها , واخزن اسمها
        // string لكن بس اخزن الصورة بالداتا بيس رح تكون 
        public string? ImageName { get; set; } // في الداتا بيس string رح تكون عبارة عن  Create بس اعملها 
    }
}
