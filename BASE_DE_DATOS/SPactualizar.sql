-- PROCEDURE: schemasye.spactualizarproducto(integer, character varying, double precision, integer, boolean)

-- DROP PROCEDURE IF EXISTS schemasye.spactualizarproducto(integer, character varying, double precision, integer, boolean);

CREATE OR REPLACE PROCEDURE schemasye.spactualizarproducto(
	IN idproducto integer,
	IN pnombre character varying,
	IN pprecio double precision,
	IN pcantidad integer,
	IN pestado boolean)
LANGUAGE 'plpgsql'
AS $BODY$
begin
update schemasye.tc_producto 
set nombre=pnombre, precio=pprecio, cantidad=pcantidad, fecha_registro=current_date,estado=pestado
where id_producto=idproducto;
end;
$BODY$;
ALTER PROCEDURE schemasye.spactualizarproducto(integer, character varying, double precision, integer, boolean)
    OWNER TO postgres;
