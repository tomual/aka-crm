CREATE TABLE [dbo].[Profile] (
    [Id]               INT            NOT NULL,
    [customerId]       INT            NOT NULL,
    [responsibleParty] NVARCHAR (MAX) NULL,
    [street]           NVARCHAR (MAX) NULL,
    [city]             NVARCHAR (50)  NULL,
    [state]            NVARCHAR (50)  NULL,
    [zip]              NVARCHAR (50)  NULL,
    [phone]            NVARCHAR (50)  NULL,
    [email]            NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

