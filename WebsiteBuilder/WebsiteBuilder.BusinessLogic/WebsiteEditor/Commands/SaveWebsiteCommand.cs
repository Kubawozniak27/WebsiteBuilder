using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.BusinessLogic.Infrastructure;
using WebsiteBuilder.Core.Service;
using WebsiteBuilder.Public.Website;
using WebsiteBuilder.Public.WebsiteEditor;
using EntityFramework.Extensions;


namespace WebsiteBuilder.BusinessLogic.WebsiteEditor.Commands
{
    public class SaveWebsiteCommand : CommandBase, ICommand<OperationResult, SaveWebsiteEditorDto>
    {
        public OperationResult Execute(SaveWebsiteEditorDto request)
        {
            var website = Db.Websites
                .Single(x => x.Id == request.WebsiteId);

            website.Color = request.WebsiteColor;

            var addWebsiteContent = request.WebsiteContents.Where(x => x.Id == null).ToList();
            var updateWebsiteContent = request.WebsiteContents.Where(x => x.Id != null).ToList();


            if(request.WidgetsToRemove != null)
            {
                foreach (var item in request.WidgetsToRemove)
                {
                    if (item.WebsiteElementType == WebsiteElementType.Image)
                    {
                        var tmp = Db.WebsiteImages.First(x => x.Id == item.Id);
                        Db.WebsiteImages.Remove(tmp);
                    }
                    else
                    {
                        var tmp = Db.WebsiteTexts.First(x => x.Id == item.Id);
                        Db.WebsiteTexts.Remove(tmp);
                    }
                }
                Db.SaveChanges();
            }
            
            if(updateWebsiteContent.Count > 0)
            {
                foreach (var item in updateWebsiteContent)
                {
                    if (item.ImageSrc != null)
                    {
                        var tmp = Db.WebsiteImages.First(x => x.Id == item.Id);
                        tmp.Height = item.Height;
                        tmp.Width = item.Width;
                        tmp.CoordinateX = item.X;
                        tmp.CoordinateY = item.Y;
                    }
                    else if (item.Text != null)
                    {
                        var tmp = Db.WebsiteTexts.First(x => x.Id == item.Id);
                        tmp.Height = item.Height;
                        tmp.Width = item.Width;
                        tmp.CoordinateX = item.X;
                        tmp.CoordinateY = item.Y;
                        tmp.Text = item.Text;
                    }

                }
                Db.SaveChanges();
            }

            if(addWebsiteContent.Count > 0)
            {
                foreach (var content in addWebsiteContent)
                {
                    if (content.WebsiteElementType == WebsiteElementType.Text)
                    {
                        var textEnitity = new Data.Entities.WebsiteText()
                        {
                            WebsiteId = request.WebsiteId,
                            CoordinateX = content.X,
                            CoordinateY = content.Y,
                            Height = content.Height,
                            Width = content.Width,
                            Text = content.Text
                        };
                        Db.WebsiteTexts.Add(textEnitity);
                    }
                    else
                    {
                        var imageEnitity = new Data.Entities.WebsiteImage()
                        {
                            CoordinateX = content.X,
                            CoordinateY = content.Y,
                            Height = content.Height,
                            Width = content.Width,
                            ImageId = content.ImageId
                        };
                        Db.WebsiteImages.Add(imageEnitity);
                    }

                }
                Db.SaveChanges();
            }

            


            return new OperationResult();
        }
    }
}
