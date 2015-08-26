<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
	$auth_dbase = $GLOBALS['auth_dbase'];
    $auth_port = $GLOBALS['auth_port'];

    $strConn = "host=$auth_host port=$auth_port dbname=$auth_dbase user=$auth_user password=$auth_pass";
	
    $db = pg_connect($strConn) or die ("Error connection: " .pg_last_error());
	
	$player_name=pg_escape_string($_POST['name']);
	
	pg_query("DELETE FROM player WHERE name = '$player_name'");
	pg_query("DELETE FROM actionbar WHERE player = '$player_name'");
	pg_query("DELETE FROM attributes WHERE player = '$player_name'");
	pg_query("DELETE FROM chest WHERE player = '$player_name'");
	pg_query("DELETE FROM equipment WHERE player = '$player_name'");
	pg_query("DELETE FROM inventory WHERE player = '$player_name'");

	pg_close($db);
?> 