<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
    $auth_port = $GLOBALS['auth_port'];

    $strConn = "host=$auth_host port=$auth_port dbname=$auth_dbase user=$auth_user password=$auth_pass";

    $db = pg_connect($strConn) or die ("Error connection: " .pg_last_error());
	
	$level = pg_escape_string($_POST['level']);
	$player = pg_escape_string($_POST['name']);
	$itemData = pg_escape_string($_POST['itemData']);

	pg_query("UPDATE player SET level = $level WHERE name = '$player'");
	pg_query("DELETE FROM attributes WHERE player= '$player'");
	pg_query("INSERT INTO attributes(player,item) VALUES ('$player','$itemData')");

	pg_close($db);
?> 