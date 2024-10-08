create table IF NOT EXISTS study
(
    ID         int auto_increment primary key,
    ProjectID  longtext    not null,
    Compound   longtext    not null,
    Sponsor    longtext    not null,
    CreatedBy  longtext    not null,
    CreatedAt  datetime(6) not null,
    ModifiedBy longtext    null,
    ModifiedAt datetime(6) null
);

