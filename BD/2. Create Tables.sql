
/*==============================================================*/
/* Table: Animal                                                */
/*==============================================================*/
create table Animal (
   AnimalId             int              identity,
   BreedId              int                  not null,
   StatusId             int                  not null,
   SexId                int                  not null,
   Name                 varchar(255)         not null,
   Birthdate            datetime             not null,
   Price                float                not null,
   constraint pk_animal primary key nonclustered (AnimalId)
)
go

/*==============================================================*/
/* Index: rgen1_fk                                              */
/*==============================================================*/
create index rgen1_fk on Animal (
BreedId asc
)
go

/*==============================================================*/
/* Index: rgen3_fk                                              */
/*==============================================================*/
create index rgen3_fk on Animal (
StatusId asc
)
go

/*==============================================================*/
/* Index: rgen2_fk                                              */
/*==============================================================*/
create index rgen2_fk on Animal (
SexId asc
)
go

/*==============================================================*/
/* Table: breed                                                 */
/*==============================================================*/
create table Breed (
   BreedId              int              identity,
   Name                 varchar(100)         not null,
   constraint pk_breed primary key nonclustered (BreedId)
)
go

/*==============================================================*/
/* Table: sex                                                   */
/*==============================================================*/
create table Sex (
   SexId                int              identity,
   Name                 varchar(10)          not null,
   constraint pk_sex primary key nonclustered (SexId)
)
go

/*==============================================================*/
/* Table: status                                                */
/*==============================================================*/
create table Status (
   StatusId             int              identity,
   Name                 varchar(100)         not null,
   constraint pk_status primary key nonclustered (StatusId)
)
go

alter table Animal
   add constraint fk_animal_rgen1_breed foreign key (BreedId)
      references Breed (BreedId)
go

alter table Animal
   add constraint fk_animal_rgen2_sex foreign key (SexId)
      references Sex (SexId)
go

alter table Animal
   add constraint fk_animal_rgen3_status foreign key (StatusId)
      references Status (StatusId)
go









/*==============================================================*/
/* Table: orderpurchase                                         */
/*==============================================================*/
create table OrderPurchase (
   OrderPurchaseId      int              identity,
   Date                 datetime             not null,
   Discount_            int                  null,
   Totalquantity        int                  null,
   Totalprice           float                null,
   Freightcharge        float                null,
   constraint pk_orderpurchase primary key nonclustered (OrderPurchaseId)
)
go




/*==============================================================*/
/* Table: orderdetail                                           */
/*==============================================================*/
create table OrderDetail (
   OrderDetailId        int              identity,
   Orderpurchaseid      int                  not null,
   Animalid             int                  not null,
   Quantity             int                  not null,
   Discount             int                  null,
   constraint pk_orderdetail primary key nonclustered (OrderDetailId)
)
go

/*==============================================================*/
/* Index: rgen4_fk                                              */
/*==============================================================*/
create index rgen4_fk on OrderDetail (
OrderPurchaseId asc
)
go

/*==============================================================*/
/* Index: rgen5_fk                                              */
/*==============================================================*/
create index rgen5_fk on OrderDetail (
AnimalId asc
)
go

alter table OrderDetail
   add constraint fk_orderdet_rgen4_orderpur foreign key (OrderPurchaseId)
      references OrderPurchase (OrderPurchaseId)
go

alter table OrderDetail
   add constraint fk_orderdet_rgen5_animal foreign key (AnimalId)
      references Animal (AnimalId)
go
