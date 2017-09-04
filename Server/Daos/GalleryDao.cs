using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Shared.Dtos.Galleries;
using PagedList;
using Server.Model;
using Shared.Core.Context.Expressions;

namespace Server.Daos
{
    public class GalleryDao : BaseDao
    {
        public GalleryDao(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public IPagedList<GalleryDto> FindPaged(GalleryFilterDto galleryFilterDto)
        {
            return _modelContext.Set<Gallery>()
                .Where(ExpressionBuilder.BuildWhere<Gallery>(galleryFilterDto))
                .OrderBy(x => x.Name)
                .Select(x => new GalleryDto()
                {
                    Id = x.Id
                })
                .ToPagedList(galleryFilterDto.Page, galleryFilterDto.PageSize);
        }

        public Gallery Find(GalleryFilterDto galleryFilterDto)
        {
            return _modelContext.Set<Gallery>()
                .Where(ExpressionBuilder.BuildWhere<Gallery>(galleryFilterDto))
                .OrderBy(x => x.Name)
                .Select(x => x)
                .SingleOrDefault();
        }
    }
}
