using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Server.Daos;
using Server.Model;
using Shared.Core.Context;
using Shared.Core.Dtos.Resources;
using Shared.Core.Utils;
using Shared.Dtos.Galleries;
using Shared.Dtos.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Generators
{
    public class ExhibitionWordGenerator : ISpecificExhibitionGenerator
    {
        private GenericDao _genericDao;
        private GalleryDao _galleryDao;
        
        public void Generate(IUnitOfWork unitOfWork, ExhibitionGenerateDto generateDto)
        {
            _genericDao = new GenericDao(unitOfWork);
            _galleryDao = new GalleryDao(unitOfWork);
            Exhibition exhibition = _genericDao.Find<Exhibition>(generateDto.ExhibitionId);
            //object missing = System.Reflection.Missing.Value;

            try
            {
                Document document = new Document();
                GenerateExhibition(document, exhibition);
                document.SaveAs2(@"d:\aaa.docx");
                document.Close();
                document = null;
            }
            catch (Exception ex)
            {
            }
        }

        private void GenerateExhibition(Document document, Exhibition exhibition)
        {
            AddParagraph(document, exhibition.Name);
            AddParagraph(document, exhibition.Description);
            GeneratePaintings(document, exhibition);
        }

        private void GeneratePaintings(Document document, Exhibition exhibition)
        {
            foreach(Painting painting in exhibition.Paintings)
            {
                GeneratePainting(document, painting);
            }
        }

        private void GeneratePainting(Document document, Painting painting)
        {
            Paragraph paragraph = document.Content.Paragraphs.Add();
            Table table = document.Tables.Add(paragraph.Range, 4, 2);
            table.Cell(1, 1).Merge(table.Cell(4, 1));
            GeneratePaintingImage(document, painting, table.Cell(1, 1));
            table.Cell(1, 2).Range.Font.Bold = 1;
            table.Cell(1, 2).Range.Text = painting.Title;
            table.Cell(2, 2).Range.Italic = 1;
            table.Cell(2, 2).Range.Text = GenerateUsers(painting.Users);
            table.Cell(3, 2).Range.Text = painting.Technique.ToString() + ", " + painting.Surface.ToString();
            table.Cell(4, 2).Range.Text = painting.Description;
            paragraph.Range.InsertParagraphAfter();
        }

        private void GeneratePaintingImage(Document document, Painting painting, Cell cell)
        {
            Gallery gallery = _galleryDao.FindSingle(new GalleryFilterDto() { OwnerId = painting.Id });
            if (gallery == null)
            {
                return;
            }
            if (gallery.CoverPhotoId == null)
            {
                return;
            }
            Resource resource = gallery.CoverPhoto;
            string imagePath = PhotoResourceDto.GetAbsoluteThumbnailFilePath(resource.Path, resource.Name);
            InlineShape inlineShape = cell.Range.InlineShapes.AddPicture(imagePath);
            inlineShape.LockAspectRatio = MsoTriState.msoTrue;
            inlineShape.Width = 180f;            
        }

        private static string GenerateUsers(ICollection<User> users)
        {
            IList<string> userNames = new List<string>();
            users.ToList().ForEach(x => userNames.Add(x.ToString()));
            return StringUtils.SeparateString(userNames, ", ");
        }

        private static void AddParagraph(Document document, string text)
        {
            Paragraph p1 = document.Content.Paragraphs.Add();
            p1.Range.Text = text;
            p1.Range.InsertParagraphAfter();
        }

        private static void AddHeader(Document document, string text)
        {
            foreach (Section section in document.Sections)
            {
                Range headerRange = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                headerRange.Fields.Add(headerRange, WdFieldType.wdFieldPage);
                headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                headerRange.Font.ColorIndex = WdColorIndex.wdBlue;
                headerRange.Font.Size = 10;
                headerRange.Text = text;
            }
        }

        private static void AddFooter(Document document, string text)
        {
            foreach (Section section in document.Sections)
            {
                Range footerRange = section.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                footerRange.Font.ColorIndex = WdColorIndex.wdDarkRed;
                footerRange.Font.Size = 10;
                footerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                footerRange.Text = text;
            }
        }
    }
}
