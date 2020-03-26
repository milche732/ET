CREATE SEQUENCE [group_sequence] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [user_sequence] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE TABLE [groups] (
    [Id] int NOT NULL,
    [Name] nvarchar(50) NULL,
    [InActive] bit NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_groups] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [users] (
    [Id] int NOT NULL,
    [Name] nvarchar(50) NULL,
    [InActive] bit NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_users] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [user_in_group] (
    [UserId] int NOT NULL,
    [GroupId] int NOT NULL,
    [InActive] bit NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_user_in_group] PRIMARY KEY ([GroupId], [UserId]),
    CONSTRAINT [FK_user_in_group_groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [groups] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_user_in_group_users_UserId] FOREIGN KEY ([UserId]) REFERENCES [users] ([Id]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_user_in_group_UserId] ON [user_in_group] ([UserId]);
GO