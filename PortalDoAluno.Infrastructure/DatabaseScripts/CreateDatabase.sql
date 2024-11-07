-- Criação do Banco de Dados
CREATE DATABASE PortalDoAlunoDB;
GO

-- Usar o Banco de Dados criado
USE PortalDoAlunoDB;
GO

-- Criação da tabela Aluno
CREATE TABLE aluno (
    id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    nome VARCHAR(255) NOT NULL,
    usuario VARCHAR(45) NOT NULL,
    senha CHAR(60) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1 -- Para indicar se o aluno está ativo
);

-- Criação da tabela Turma
CREATE TABLE turma (
    id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    curso_id INT NOT NULL,
    turma VARCHAR(45) NOT NULL,
    ano INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1 -- Para indicar se a turma está ativa
);

-- Criação da tabela de relacionamento Aluno_Turma
CREATE TABLE aluno_turma (
    aluno_id INT NOT NULL,
    turma_id INT NOT NULL,
    PRIMARY KEY (aluno_id, turma_id),
    FOREIGN KEY (aluno_id) REFERENCES aluno(id),
    FOREIGN KEY (turma_id) REFERENCES turma(id),
    IsActive BIT NOT NULL DEFAULT 1 -- Para indicar se a relação está ativa
);
