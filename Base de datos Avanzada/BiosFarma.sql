--creo la base de datos de ventas
use master
go

if exists(Select * FROM SysDataBases WHERE name='BiosFarma')
BEGIN
	DROP DATABASE BiosFarma
END
go

CREATE DATABASE BiosFarma
go

USE BiosFarma
GO

-------------------------------------- DUDAS ------------------------------------------

-- Ayuda con cambio pass
-- Permisos IISAPPPOOL

-------------------------------------- TABLAS -----------------------------------------
CREATE TABLE Farmaceutica
(
Nombre VARCHAR(20) NOT NULL PRIMARY KEY,
Direccion VARCHAR(100) NOT NULL,
Telefono VARCHAR(10) NOT NULL,
Email VARCHAR(40) NOT NULL,
Activo bit
)
GO

CREATE TABLE Medicamento
(
Codigo VARCHAR(10) NOT NULL,
NomFarmaceutica VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES Farmaceutica(Nombre),
NomMedicamento VARCHAR(20) NOT NULL,
Descripcion VARCHAR(100),
TipoMedicamento VARCHAR(20) NOT NULL CHECK(TipoMedicamento = 'Cardiologico' OR TipoMedicamento = 'Diabeticos' OR TipoMedicamento = 'Otros'),
Stock INT NOT NULL,
Precio MONEY,
Activo bit,
PRIMARY KEY(Codigo, NomFarmaceutica)
)
GO 

CREATE TABLE Usuario
(
NomLogueo VARCHAR(10) NOT NULL UNIQUE,
Pass VARCHAR(7) CHECK (Pass LIKE '[A-Z][A-Z][A-Z][A-Z][A-Z][0-9][0-9]'),
Documento INTEGER NOT NULL PRIMARY KEY,
NombreCompleto VARCHAR(20) NOT NULL
)
GO

CREATE TABLE Encargado
(
Documento INTEGER NOT NULL FOREIGN KEY REFERENCES Usuario(Documento),
Telefono  VARCHAR(10) NOT NULL
)
GO

CREATE TABLE Empleado
(
Documento INTEGER NOT NULL FOREIGN KEY REFERENCES Usuario(Documento),
HoraInicio DATETIME NOT NULL,
HoraFinal DATETIME NOT NULL,
Activo bit
)
GO

CREATE TABLE HorasExtra
(
Documento INT NOT NULL FOREIGN KEY REFERENCES Usuario(Documento),
Fecha DATETIME NOT NULL,
Minutos INT NOT NULL
)

CREATE TABLE Pedido
(
NumPedido INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,
FechaPedido DATETIME NOT NULL,
Direccion VARCHAR(100) NOT NULL,
Estado VARCHAR(10) DEFAULT 'Generado' CHECK(Estado='Generado' OR Estado='Entregado' OR Estado='Enviado') ,
UsuarioPedido INTEGER NOT NULL FOREIGN KEY REFERENCES Usuario(Documento)
)
GO

Create Table PedidoMedicamento
(
NumPedido INTEGER NOT NULL FOREIGN KEY REFERENCES Pedido(NumPedido),
CodigoMed VARCHAR(10) NOT NULL,
NomFarma VARCHAR(20) NOT NULL,
Cantidad INT NOT NULL,
PRIMARY KEY(NumPedido, CodigoMed, NomFarma)
)
GO

ALTER TABLE PedidoMedicamento
ADD CONSTRAINT FK_PedidoMedicamento foreign key (CodigoMed, NomFarma) References Medicamento(Codigo, NomFarmaceutica)
go

------------------------------------------ procedimientos usuarios -------------------------------------

Create  Proc AltaEmpleado
@NomLogueo varchar(10),
@pass varchar(7),
@documento int,
@nombre varchar(20),
@horainicio datetime,
@horafinal datetime
as
begin
	if (exists(select * from Empleado where @documento = Documento AND Activo = 1))
	return -1
else if(exists(select * from Usuario where NomLogueo = @NomLogueo))
	return -3
		if(exists(select * from Empleado where @documento = Documento AND Activo = 0))
		begin
			update Usuario set NombreCompleto = @nombre where @documento = Documento
				update Empleado set HoraInicio = @horainicio, HoraFinal = @horafinal, Activo = 1 where @documento = Documento
		end
		else	
			begin transaction tran1
				begin
					insert into Usuario(NomLogueo, pass, Documento, NombreCompleto) values (@NomLogueo, @pass, @documento, @nombre)
					if(@@ERROR <>  0)
					begin
						rollback
						return -2
					end
					else
						begin
								insert into empleado(Documento, HoraInicio, HoraFinal, Activo) values (@documento, @horainicio, @horafinal, 1)
								if(@@ERROR <>  0)
								begin
									rollback
									return -2
								end
									begin transaction tran2
										Declare @VarSentencia varchar(200)
											Set @VarSentencia = 'CREATE LOGIN [' +  @NomLogueo + '] WITH PASSWORD = ' + QUOTENAME(@pass, '''')
												Exec (@VarSentencia)
												if (@@ERROR <> 0)
												begin
													rollback
													return -4
												end
												else
													Set @VarSentencia = 'Create User [' +  @NomLogueo + '] From Login [' + @NomLogueo + ']'
														Exec (@VarSentencia)
														if (@@ERROR <> 0)
														begin
															rollback
															return -4
														end
														else
															Set @VarSentencia = 'GRANT INSERT ON PEDIDO TO [' +  @NomLogueo + ']'
																Exec (@VarSentencia)
																if (@@ERROR <> 0)
																	begin
																		rollback
																		return -4
																	end
																	else
																		Set @VarSentencia = 'GRANT ALL ON USUARIO TO [' +  @NomLogueo + ']'
																			Exec (@VarSentencia)
																			if (@@ERROR <> 0)
																			begin
																				rollback
																				return -4
																			end
																			else
																				Set @VarSentencia = 'GRANT ALL ON EMPLEADO TO [' +  @NomLogueo + ']'
																					Exec (@VarSentencia)
																					if (@@ERROR <> 0)
																					begin
																						rollback
																						return -4
																					end
																					else
																						Set @VarSentencia = 'GRANT EXECUTE ON dbo.AltaPedido TO [' +  @NomLogueo + ']'
																							Exec (@VarSentencia)
																							if (@@ERROR <> 0)
																							begin
																								rollback
																								return -4
																							end
																							else
																								Set @VarSentencia = 'GRANT EXECUTE ON dbo.Logueo TO [' +  @NomLogueo + ']'
																									Exec (@VarSentencia)
																									if (@@ERROR <> 0)
																									begin
																										rollback
																										return -4
																									end
																									else
																										Set @VarSentencia = 'GRANT EXECUTE ON dbo.CambioPassword TO [' +  @NomLogueo + ']'
																											Exec (@VarSentencia)
																											if (@@ERROR <> 0)
																											begin
																												rollback
																												return -4
																											end
																											else
																												Set @VarSentencia = 'GRANT EXECUTE ON dbo.LineaPedidos TO [' +  @NomLogueo + ']'
																													Exec (@VarSentencia)
																													if (@@ERROR <> 0)
																													begin
																														rollback
																														return -4
																													end
																													else
																														Set @VarSentencia = 'GRANT EXECUTE ON dbo.ListarMedicamentos TO [' +  @NomLogueo + ']'
																															Exec (@VarSentencia)
																															if (@@ERROR <> 0)
																															begin
																																rollback
																																return -4
																															end
																															else
																														commit
																													commit
																													return 1
																												end
																											end
																										end
go


Create Proc AltaEncargado
@NomLogueo varchar(10),
@pass varchar(7),
@documento int,
@nombre varchar(20),
@telefono varchar(10)
as
begin
	if (exists(select * from Encargado where @documento = Documento))
	return -1
	else if(exists(select * from Usuario where NomLogueo = @NomLogueo))
	return -3
else
	begin
		begin transaction tran1
			insert into Usuario(NomLogueo, pass, Documento, NombreCompleto) values (@NomLogueo, @pass, @documento, @nombre)
			if(@@ERROR <>  0)
			begin
				rollback
				return -2
			end
			else
				begin
					insert into Encargado(Documento, Telefono) values (@documento, @telefono)
					if(@@ERROR <>  0)
					begin
						rollback
						return -2
					end
					else
						begin
							begin transaction tran2
								Declare @VarSentencia varchar(200)
								Set @VarSentencia = 'CREATE LOGIN [' +  @NomLogueo + '] WITH PASSWORD = ' + QUOTENAME(@pass, '''')
								Exec (@VarSentencia)
								if (@@ERROR <> 0)
								begin
									rollback
									return -4
								end
								else
									Set @VarSentencia = 'Create User [' +  @NomLogueo + '] From Login [' + @NomLogueo + ']'
									Exec (@VarSentencia)
									
												if (@@ERROR <> 0)
												begin
													ROLLBACK
													return -4
												end
												else
													Set @VarSentencia = 'GRANT EXECUTE TO [' +  @NomLogueo + ']'
													Exec (@VarSentencia)
													if (@@ERROR <> 0)
													begin
														rollback
														return -4
													end
														commit
															commit
																begin
																	Exec sp_addrolemember @rolename='db_owner', @membername=@NomLogueo
																	Exec sp_addsrvrolemember @NomLogueo, 'sysadmin'
																	return 1
																	end
																end
															end
														end
													end
go	


Create Proc ModEmpleado
@documento int,
@nombre varchar(20),
@horainicio datetime,
@horafinal datetime
as
begin
	if (not exists(select * from Empleado where @documento = Documento AND Activo = 1))
		return -1
		else
			begin
				begin transaction
			update Empleado set HoraInicio = @horainicio, HoraFinal = @horafinal where @documento = Documento
			if(@@ERROR <>  0)
			begin
				rollback
				return -2
			end
				else
				begin
					update Usuario set NombreCompleto = @nombre where @documento = Documento
					if(@@ERROR <>  0)
					begin
						rollback
						return -2
					end
					else
					commit
				return 1
			end
		end
	end
go

Create Proc BajaEmpleado
@documento int,
@nomLogueo varchar(10)
as
begin
	if (not exists(select * from Empleado where @documento = Documento))
		return -1
		else
		begin
		if(exists(select * from Pedido where UsuarioPedido = @documento))
			update Empleado set Activo = 0 where @documento = Documento
			if(@@ERROR <>  0)
			begin
				rollback
				return -2
			end
				else
				begin
					begin transaction tran1
					delete from Empleado where @documento = Documento
					if(@@ERROR <>  0)
					begin
						rollback
						return -2
					end
						else
						begin
							delete from HorasExtra where @documento = Documento
							if(@@ERROR <>  0)
						begin
							rollback
							return -2
						end
							else
							begin
								delete from Usuario where @documento = Documento
								if(@@ERROR <>  0)
								begin
									rollback
									return -2
								end
									else
									begin
										begin transaction tran2
											Declare @VarSentencia varchar(200)
												Set @VarSentencia = 'DROP LOGIN [' +  @NomLogueo + ']'
													Exec (@VarSentencia)
													if (@@ERROR <> 0)
													begin
														rollback
														return -4
													end
													else
														Set @VarSentencia = 'REVOKE INSERT ON PEDIDO TO [' +  @NomLogueo + ']'
															Exec (@VarSentencia)
															if (@@ERROR <> 0)
															begin
																rollback
																return -4
															end
																else
																	Set @VarSentencia = 'REVOKE INSERT ON HORASEXTRA TO [' +  @NomLogueo + ']'
																		Exec (@VarSentencia)
																		if (@@ERROR <> 0)
																		begin
																			rollback
																			return -4
																		end
																		else
																			Set @VarSentencia = 'REVOKE ALL ON USUARIO TO [' +  @NomLogueo + ']'
																				Exec (@VarSentencia)
																				if (@@ERROR <> 0)
																				begin
																					rollback
																					return -4
																					end
																				else
																					Set @VarSentencia = 'REVOKE ALL ON EMPLEADO TO [' +  @NomLogueo + ']'
																						Exec (@VarSentencia)
																						if (@@ERROR <> 0)
																						begin
																							rollback
																							return -4
																						end
																						else
																				commit
																		commit
																return 1
															end
														end
													end
												end
											end
										end
go

Create Proc CambioPassword
@passActual varchar(7),
@nuevaPass varchar(7),
@logueo varchar(20)
as
begin
	if (not exists(select * from Usuario where NomLogueo = @logueo))
		return -1
		else
			begin
				update Usuario set Pass = @nuevaPass where NomLogueo = @logueo
					exec sp_password @passActual, @nuevaPass, @logueo
					return 1
				end
			end
go

Create Proc Logueo
@NomLogueo varchar(10),
@pass varchar(7)
as
	if(not exists(select documento from Usuario where @nomlogueo = nomlogueo))
		return -1
			begin
				declare @doc int
					select @doc = documento from Usuario where @nomlogueo = nomlogueo
						if(exists(select * from Empleado where @doc = Documento))
							select * from Empleado E, Usuario U where U.NomLogueo = @NomLogueo AND E.Documento = @doc AND E.Documento = U.Documento AND E.Activo = 1
						else
							select * from Encargado E, Usuario U where U.NomLogueo = @NomLogueo AND E.Documento = @doc AND E.Documento = U.Documento
						end
go
	
Create Proc AgregarExtras
@documento int,
@fecha datetime,
@minutos int
as
if(exists(select * from HorasExtra  where documento = @documento AND fecha = @fecha))
	update HorasExtra set Minutos = @minutos where Documento = @documento and Fecha = @fecha
else
	insert into HorasExtra (Documento, Fecha, Minutos) values (@documento, @fecha, @minutos)
go

Create Proc ListarEmpleadosAct
as
select * from Empleado E, Usuario U where E.Documento = U.Documento and E.Activo = 1
go

Create Proc ListarEncargados
as
select * from Encargado E, Usuario U where E.Documento = U.Documento
go

Create Proc BuscarEmpleadoAct
@documento int
as
select * from Empleado E, Usuario U where @documento = E.Documento AND @documento = U.Documento AND E.Activo = 1
go

Create Proc BuscarEmpleadoTodos
@documento int
as
select * from Empleado E, Usuario U where @documento = E.Documento AND @documento = U.Documento
go

Create Proc BuscarEncargado
@documento int
as
select * from Encargado E, Usuario U where @documento = E.Documento AND @documento = U.Documento
go

-----------------------------------Procedimientos Farmaceutica--------------------------------------

Create Proc AltaFarmaceutica
@nombre varchar(20),
@direccion varchar(100),
@telefono varchar(10),
@email varchar(40)
as
	if(exists(select * from Farmaceutica where @nombre = Nombre and activo = 1))
		return -1
	else 
		if(exists(select * from Farmaceutica where @nombre = Nombre and activo = 0))
			update Farmaceutica set Direccion = @direccion, Telefono = @telefono, Email = @email, Activo=1
		else
			Insert into Farmaceutica(Nombre, Direccion, Telefono, Email, Activo) values (@nombre, @direccion, @telefono, @email, 1)
go
			
Create Proc BajaFarmaceutica
@nombre varchar(20)
as
begin
		if(not exists(select * from Farmaceutica where @nombre = Nombre))
			return -1
		else if(exists(select * from PedidoMedicamento where @nombre = NomFarma))
			update Farmaceutica set Activo = 0 where @nombre = Nombre
		else
			begin
				begin transaction
					delete from Medicamento where NomFarmaceutica = @nombre
					if(@@ERROR <>  0)
						begin
						rollback
						return -2
					end
					else
						delete from Farmaceutica where Nombre = @nombre
						if(@@ERROR <>  0)
							begin
							rollback
							return -2
						end
						else
							commit
							return 1
						end
					end			
			
go

Create Proc ModFarmaceutica
@nombre varchar(20),
@direccion varchar(100),
@telefono varchar(10),
@email varchar(40)
as
	if(not exists(select * from Farmaceutica where @nombre = Nombre and activo = 1))
		select -1
	else
		update Farmaceutica set Direccion = @direccion, Telefono = @telefono, Email = @email where @nombre = Nombre
go

Create Proc BuscoFarmaceutica
@nombre varchar(20)
as
select * from Farmaceutica where @nombre = Nombre AND activo = 1
go

Create Proc BuscoFarmaceuticaTodas
@nombre varchar(20)
as
select * from Farmaceutica where @nombre = Nombre
go

Create Proc ListarFarmaceuticaActiva
as
select * from Farmaceutica where  activo = 1
go

Create Proc ListarFarmaceutica
as
select * from Farmaceutica
go


------------------------------------------- Procedimientos Medicamentos -------------------------------------------------

Create Proc AltaMedicamento
@codigo varchar(10),
@nomFarma varchar(20),
@nomMedicamento varchar(20),
@descripcion varchar(100),
@tipoMedicamento varchar(20),
@precio money,
@stock int
as
if(exists(select * from Medicamento where @nomFarma = NomFarmaceutica AND @codigo = Codigo and Activo = 1))
	return -1
else if(exists(select * from Medicamento where @nomFarma = NomFarmaceutica AND @codigo = Codigo and Activo = 0))
	update Medicamento set Activo = 1, Stock=@stock, Precio=@precio where @nomFarma = NomFarmaceutica AND @codigo = Codigo
else
	insert into Medicamento(Codigo, NomFarmaceutica, NomMedicamento, Descripcion, TipoMedicamento, Precio, Stock, Activo) values (@codigo, @nomFarma, @nomMedicamento, @descripcion, @tipoMedicamento, @precio, @stock, 1)
go



Create Proc BajaMedicamento
@codigo varchar(10),
@nomFarma varchar(20)
as
if(not exists(select * from Medicamento where @nomFarma = NomFarmaceutica AND @codigo = codigo and Activo = 1))
	return -1
else
	begin
		if(exists(select * from PedidoMedicamento where CodigoMed = @codigo AND NomFarma = @nomFarma))
			update Medicamento set Activo = 0 where @nomFarma = NomFarmaceutica AND @codigo = codigo and Activo = 1
		else
			delete from Medicamento where @nomFarma = NomFarmaceutica AND @codigo = codigo and Activo = 1
		end
				
go

Create Proc ModMedicamento
@codigo varchar(10),
@nomFarma varchar(20),
@descripcion varchar(100),
@tipoMedicamento varchar(20),
@precio money,
@stock int
as
begin
if(not exists(select * from Medicamento where @nomFarma = NomFarmaceutica AND @codigo = codigo and Activo = 1))
	return -1
else
	update Medicamento Set Descripcion = @descripcion, TipoMedicamento = @tipoMedicamento, Precio = @precio, Stock = @stock where NomFarmaceutica = @nomFarma AND Codigo = @codigo
end
go

Create Proc BuscarMedicamentoActivo
@codigo varchar(10),
@nomFarma varchar(20)
as
select * from Medicamento where @nomFarma = NomFarmaceutica AND @codigo = codigo and Activo = 1
go

Create Proc BuscarMedicamentoTodas
@codigo varchar(10),
@nomFarma varchar(20)
as
select * from Medicamento where @nomFarma = NomFarmaceutica AND @codigo = codigo
go

Create Proc ListarMedicamentosTodas
as
select * from Medicamento
go

Create Proc ListarMedicamentos
as
select * from Medicamento where Activo = 1 AND Stock > 0
go

------------------------------------------ Procedimientos Pedidos ----------------------------------------------------------

Create Proc AltaPedido
@fechapedido datetime,
@direccion varchar(100),
@usuariopedido int
as
begin
	insert into Pedido (FechaPedido, Direccion, UsuarioPedido, Estado) values (@fechapedido, @direccion, @usuariopedido, 'Generado')
	return @@IDENTITY
end
go



Create Proc CambioEstadoPedido -- paso al siguiente automaticamente
@numPedido int
as
if(select Estado from Pedido where NumPedido = @numPedido) = 'Generado'
	update Pedido set Estado = 'Enviado' where NumPedido = @numPedido
else
	update Pedido set Estado = 'Entregado' where NumPedido = @numPedido
go

Create Proc BuscarPedido
@numPedido int
as
select * from Pedido P, PedidoMedicamento M where P.NumPedido = @numPedido AND M.NumPedido = @numPedido
go

Create Proc ListarPedidos
as
select * from Pedido P, PedidoMedicamento M where P.NumPedido = M.NumPedido
go

Create Proc LineaPedidos
@numPedido int,
@codigomed varchar(10),
@nomfarma varchar(20),
@cantidad int
as
begin 
	if((select stock from Medicamento where NomFarmaceutica = @nomfarma and Codigo = @codigomed) < @cantidad)
		return -1
	else
		begin
			begin transaction
				insert into PedidoMedicamento (NumPedido, CodigoMed, NomFarma, Cantidad) values (@numPedido, @codigomed, @nomfarma, @cantidad)
				if(@@ERROR <> 0)
				begin
					rollback
					return -2
				end
				else
					update Medicamento set Stock = Stock - @cantidad where NomFarmaceutica = @nomFarma AND Codigo = @codigomed
					if(@@ERROR <> 0)
					begin
						rollback
						return -2
					end
				else
					commit
					return 1
			end
		end
go

--------------------------------- CREACION USUARIO IISAPPPOOL ------------------------------------------------
-- Ajustar permisos IIS, solamente permisos asociados para las acciones

USE BiosFarma
GO

CREATE LOGIN BiosFarma WITH Password = 'BiosFarma'
GO


CREATE USER BiosFarma FOR LOGIN BiosFarma
GO

GRANT Execute On dbo.ListarMedicamentos to BiosFarma
go

GRANT Execute On dbo.BuscarEmpleadoTodos to BiosFarma
go

GRANT Execute On dbo.BuscarMedicamentoTodas to BiosFarma
go	

GRANT Execute On dbo.BuscoFarmaceutica to BiosFarma
go

GRANT Execute On dbo.BuscoFarmaceuticaTodas to BiosFarma
go

GRANT Execute On dbo.Logueo to BiosFarma
go

GRANT Execute On dbo.AgregarExtras to BiosFarma
go

GRANT Execute on dbo.LineaPedidos to BiosFarma
go

GRANT Execute on dbo.BuscarPedido to BiosFarma
go

exec AltaEncargado "alvaro", "aaaaa11", 38495916, "alvaro", "23232323"
go

exec  AltaEmpleado "ajose", "aaaaa11", 384495916, "jose","15:00", "17:00"

go
select *
from Empleado