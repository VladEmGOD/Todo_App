using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Buisness.Models;
using Buisness.Repositories.Interfaces;

namespace TODO_APP.Repositories.XML
{
    public class XMLCategoriesRepository : ICategoriesRerository
    {
        public string fileName = "Categories.xml";
        public string filePash = @"C:\Study\SIC\TODO_APP\TODO_APP\XMLData\Categories.xml";
        XmlSerializer xmlSerializer;
        public XMLCategoriesRepository()
        {
            xmlSerializer = new XmlSerializer(typeof(List<CategoryModel>));
        }

        public Task CreateAsync(CategoryModel categoryModel)
        {
            List<CategoryModel> categories;

            using (FileStream fs = new FileStream(filePash, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read, 4096, true))
            {
                categories = (List<CategoryModel>?)xmlSerializer.Deserialize(fs);
                if (categories == null)
                    categories = new List<CategoryModel>();
            }

            categoryModel.Id = categories.Last().Id + 1;

            categories.Add(categoryModel);

            using (FileStream fs = new FileStream(filePash, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write, 4096, true))
            {
                xmlSerializer.Serialize(fs, categories);
                return Task.FromResult(categories);
            }
        }

        public Task DeleteAsync(int id)
        {
            List<CategoryModel> categories;
            CategoryModel selectedCategory;

            using (FileStream fs = new FileStream(filePash, FileMode.OpenOrCreate))
            {
                categories = (List<CategoryModel>)xmlSerializer.Deserialize(fs);
                if (categories == null)
                    categories = new List<CategoryModel>();

                selectedCategory = categories.SingleOrDefault(t => t.Id == id);
            }

            categories.Remove(selectedCategory);

            using (FileStream fs = new FileStream(filePash, FileMode.Truncate))
            {
                xmlSerializer.Serialize(fs, categories);
            }

            return Task.FromResult(selectedCategory);
        }

        public Task EditAsync(CategoryModel category)
        {
            List<CategoryModel> categories;
            CategoryModel selectedCategory;
            var xmlSerializerOne = new XmlSerializer(typeof(List<CategoryModel>));

            using (FileStream fs = new FileStream(filePash, FileMode.OpenOrCreate))
            {
                categories = (List<CategoryModel>?)xmlSerializer.Deserialize(fs);
                if (categories == null)
                    categories = new List<CategoryModel>();

                selectedCategory = categories.SingleOrDefault(t => t.Id == category.Id);
            }

            using (FileStream fs = new FileStream(filePash, FileMode.Truncate))
            {
                int categoryIndex = categories.FindIndex(c => c.Id == category.Id);
                categories[categoryIndex] = category;
                xmlSerializerOne.Serialize(fs, categories);
            }
            return Task.FromResult(category);
        }

        public Task<IEnumerable<CategoryModel>> GetCategoriesAsync()
        {
            using (FileStream fs = new FileStream(filePash, FileMode.OpenOrCreate))
            {
                IEnumerable<CategoryModel> categories = (List<CategoryModel>?)xmlSerializer.Deserialize(fs);
                if (categories == null)
                    categories = new List<CategoryModel>();
                return Task.FromResult(categories);
            }
        }

        public Task<CategoryModel> GetCategoryByIdAsync(int id)
        {
            List<CategoryModel> categories;

            using (FileStream fs = new FileStream(filePash, FileMode.OpenOrCreate))
            {
                categories = (List<CategoryModel>?)xmlSerializer.Deserialize(fs);
                if (categories == null)
                    categories = new List<CategoryModel>();

                var resultCategory = categories.SingleOrDefault(t => t.Id == id);
                return Task.FromResult(resultCategory);
            }
        }

    }
}
