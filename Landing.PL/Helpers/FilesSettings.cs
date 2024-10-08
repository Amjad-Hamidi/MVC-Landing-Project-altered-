namespace Landing.PL.Helpers
{
	public class FilesSettings
	{
		public static string UploadFile(IFormFile file,string folderName) {
			//string folderPath = Directory.GetCurrentDirectory() + "" + folderName; // عشان اذا رفعته عسيرفر مش رح احتاج بلزمش اعمل اي تغيير على المسار
			// OR :
			string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName); // (+) بقدر ابعثله باراميتر ثالث (اسم الفايل) بس مش رح يكون مشفر , هسا بدي اشفره //  هاي الطريقة افضل, لانو مرات السيرفر ما بتعرف على  
			string fileName = $"{Guid.NewGuid()} {file.FileName}"; // عشوائي عشان يفيدني بالتشفير في حال اكثر من يوزر ضافو بنفس الوقت نفس اسم الملف , وبضيف عليه اسم الملف , هيك بضمن التشفير Id بعطيه 
			string filePath = Path.Combine (folderPath, fileName);

			var fileStream = new FileStream(filePath,FileMode.Create); // انشاء فايل للقراءة		
			file.CopyTo(fileStream);

			return fileName;
		}

		public static void DeleteFile(string fileName , string folderName) {
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", fileName);
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}


		}
	}
}
