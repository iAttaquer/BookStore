CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'dotNetBoilerplate') THEN
        CREATE SCHEMA "dotNetBoilerplate";
    END IF;
END $EF$;

CREATE TABLE "dotNetBoilerplate"."OutboxMessages" (
    "Id" uuid NOT NULL,
    "OccurredOn" timestamp with time zone NOT NULL,
    "Type" text NOT NULL,
    "Data" text NOT NULL,
    "ProcessedDate" timestamp with time zone,
    CONSTRAINT "PK_OutboxMessages" PRIMARY KEY ("Id")
);

CREATE TABLE "dotNetBoilerplate"."Users" (
    "Id" uuid NOT NULL,
    "Email" text,
    "Username" text,
    "Password" character varying(200) NOT NULL,
    "Role" character varying(30) NOT NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    "AccountType" text NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

CREATE UNIQUE INDEX "IX_Users_Email" ON "dotNetBoilerplate"."Users" ("Email");

CREATE UNIQUE INDEX "IX_Users_Username" ON "dotNetBoilerplate"."Users" ("Username");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240305104537_Initial', '8.0.2');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240724140223_Add-BookStores', '8.0.2');

COMMIT;

START TRANSACTION;

CREATE TABLE "dotNetBoilerplate"."Books" (
    "Id" uuid NOT NULL,
    "Title" character varying(30) NOT NULL,
    "Writer" character varying(30) NOT NULL,
    "Genre" character varying(20) NOT NULL,
    "Year" integer NOT NULL,
    "Description" character varying(1000),
    "BookStoreId" uuid NOT NULL,
    "CreatedBy" uuid NOT NULL,
    CONSTRAINT "PK_Books" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Books_BookStores_BookStoreId" FOREIGN KEY ("BookStoreId") REFERENCES "dotNetBoilerplate"."BookStores" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Books_Users_CreatedBy" FOREIGN KEY ("CreatedBy") REFERENCES "dotNetBoilerplate"."Users" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Books_BookStoreId" ON "dotNetBoilerplate"."Books" ("BookStoreId");

CREATE INDEX "IX_Books_CreatedBy" ON "dotNetBoilerplate"."Books" ("CreatedBy");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240729122724_Add-Book', '8.0.2');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240729185816_Add-Reviews', '8.0.2');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240730142621_dfdsf', '8.0.2');

COMMIT;

