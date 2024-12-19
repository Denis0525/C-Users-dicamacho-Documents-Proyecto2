-- FUNCTION: schemasye.obtener_productos()

-- DROP FUNCTION IF EXISTS schemasye.obtener_productos();

CREATE OR REPLACE FUNCTION schemasye.obtener_productos(
	)
    RETURNS TABLE(id_producto integer, nombre character varying, precio double precision, cantidad integer, fecha_registro date, estado boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY SELECT * FROM schemasye.tc_producto ORDER BY id_producto ASC;
END;
$BODY$;

ALTER FUNCTION schemasye.obtener_productos()
    OWNER TO postgres;
