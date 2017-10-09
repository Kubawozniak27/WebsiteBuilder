CREATE TABLE [dbo].[Websites](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[PublishDate] [datetime] NULL, 
    CONSTRAINT [PK_Website] PRIMARY KEY ([Id]),
 )