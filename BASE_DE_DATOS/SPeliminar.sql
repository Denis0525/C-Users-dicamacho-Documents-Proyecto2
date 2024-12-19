-- PROCEDURE: schemasye.speliminarproducto(integer)

-- DROP PROCEDURE IF EXISTS schemasye.speliminarproducto(integer);

CREATE OR REPLACE PROCEDURE schemasye.speliminarproducto(
	IN id integer)
LANGUAGE 'plpgsql'
AS $BODY$
begin
	delete from schemasye.tc_producto where id_producto=id;
end;
$BODY$;
ALTER PROCEDURE schemasye.speliminarproducto(integer)
    OWNER TO postgres;

GRANT EXECUTE ON PROCEDURE schemasye.speliminarproducto(integer) TO PUBLIC;

GRANT EXECUTE ON PROCEDURE schemasye.speliminarproducto(integer) TO postgres WITH GRANT OPTION;

