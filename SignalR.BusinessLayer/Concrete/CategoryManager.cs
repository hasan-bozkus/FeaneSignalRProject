﻿using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Concrete
{
	public class CategoryManager : ICategoryService
	{
		private readonly ICategoryDal _CategoryDal;

		public CategoryManager(ICategoryDal CategoryDal)
		{
			_CategoryDal = CategoryDal;
		}

        public int TActiveCategoryCount()
        {
            return _CategoryDal.ActiveCategoryCount();
        }

        public void TAdd(Category entity)
		{
			_CategoryDal.Add(entity);
		}

        public int TCategoryCount()
        {
            return _CategoryDal.CategoryCount();
        }

        public void TDelete(Category entity)
		{
			_CategoryDal.Delete(entity);
		}

		public Category TGetByID(int id)
		{
			return _CategoryDal.GetByID(id);
		}

		public List<Category> TGetListAll()
		{
			return _CategoryDal.GetListAll();
		}

        public int TPassiveCategoryCount()
        {
            return _CategoryDal.PassiveCategoryCount();
        }

        public void TUpdate(Category entity)
		{
			_CategoryDal.Update(entity);
		}
	}
}
