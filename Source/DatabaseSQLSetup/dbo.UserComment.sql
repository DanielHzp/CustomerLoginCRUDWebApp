CREATE TABLE [dbo].[UserComment] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [CommentDescription] NVARCHAR (MAX) NULL,
    [Details]            NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserComment] PRIMARY KEY CLUSTERED ([Id] ASC)
);

