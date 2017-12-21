using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.BusinessLogic.Infrastructure;
using WebsiteBuilder.Core.Service;
using WebsiteBuilder.Public.WebsiteEditor;


namespace WebsiteBuilder.BusinessLogic.WebsiteEditor.Commands
{
    public class SaveWebsiteCommand : CommandBase, ICommand<OperationResult, SaveWebsiteEditorDto>
    {
        public OperationResult Execute(SaveWebsiteEditorDto request)
        {
            var website = Db.Websites
                .Single(x => x.Id == request.WebsiteId);

            website.Color = request.WebsiteColor;

            var addWebsiteText = request.Texts.Where(x => x.Id == null).ToList();
            var updateWebsiteText = request.Texts.Where(x => x.Id != null).ToList();

            foreach (var item in updateWebsiteText)
            {
                var tmp = Db.WebsiteTexts.Single(x => x.Id == item.Id);

                tmp.Height = item.Height;
                tmp.Width = item.Width;
                tmp.CoordinateX = item.X;
                tmp.CoordinateY = item.Y;
                tmp.Text = item.Text;
            }
            Db.SaveChanges();

            foreach (var text in addWebsiteText)
            {
                var textEnitity = new Data.Entities.WebsiteText()
                {
                    WebsiteId = request.WebsiteId,
                    CoordinateX = text.X,
                    CoordinateY = text.Y,
                    Height = text.Height,
                    Width = text.Width,
                    Text = text.Text
                };
                Db.WebsiteTexts.Add(textEnitity);
            }
            Db.SaveChanges();


            return new OperationResult();
        }
    }
}
