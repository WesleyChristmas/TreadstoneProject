CREATE TABLE [dbo].[UserRoles] (
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId] UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId]),
    CONSTRAINT [FK__UserRoles__UserI__0E6E26BF] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
);

