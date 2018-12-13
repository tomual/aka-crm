CREATE TABLE [dbo].[Company] (
    [id]       INT            IDENTITY (1, 1) NOT NULL,
    [name]     NVARCHAR (MAX) NOT NULL,
    [created]  DATETIME       CONSTRAINT [DF_Company_created_at] DEFAULT (getdate()) NOT NULL,
    [modified] DATETIME       CONSTRAINT [DF_Company_modified_at] DEFAULT (getdate()) NOT NULL
);

