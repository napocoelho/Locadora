use locadora;


-- https://localhost:7181/api/testes/teste



drop table if exists Cliente;
drop table if exists Filme;
drop table if exists Locacao;




create table if not exists Cliente
(
	Id 				int not null auto_increment,
    Nome 			varchar(200),
    CPF 			varchar(11),
    DataNascimento 	datetime,
    
    PRIMARY KEY (Id)
);

ALTER TABLE Cliente ADD UNIQUE (Nome);
ALTER TABLE Cliente ADD UNIQUE (CPF);


create table if not exists Filme
(
	Id 						int not null auto_increment,
    Titulo 					varchar(100),
    ClassificacaoIndicativa int,
    Lancamento 				tinyint,
    
    PRIMARY KEY (Id)
);

ALTER TABLE Filme ADD UNIQUE (Titulo);
/*ALTER TABLE Filme ADD UNIQUE (Lancamento);*/


create table if not exists Locacao
(
	Id				int not null auto_increment,
    Id_Cliente 		int,
    Id_Filme 		int,
    DataLocacao 	datetime,
    DataDevolucao 	datetime,
    
    PRIMARY KEY (Id),
    FOREIGN KEY (Id_Cliente) REFERENCES Cliente(Id),
    FOREIGN KEY (Id_Filme) REFERENCES Filme(Id)
);
