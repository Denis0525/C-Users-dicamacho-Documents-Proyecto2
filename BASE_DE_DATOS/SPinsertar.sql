-- PROCEDURE: schemasye.spinsertarproducto(character varying, double precision, integer, boolean)

-- DROP PROCEDURE IF EXISTS schemasye.spinsertarproducto(character varying, double precision, integer, boolean);

CREATE OR REPLACE PROCEDURE schemasye.spinsertarproducto(
	IN nombre character varying,
	IN precio double precision,
	IN cantidad integer,
	IN estado boolean)
LANGUAGE 'plpgsql'
AS $BODY$
begin
insert into schemasye.tc_producto(
nombre, precio, cantidad, fecha_registro, 
estado)values(nombre,precio,cantidad,current_date,estado);
end;
$BODY$;
ALTER PROCEDURE schemasye.spinsertarproducto(character varying, double precision, integer, boolean)
    OWNER TO postgres;
