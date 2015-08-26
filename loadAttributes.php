<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
    $auth_port = $GLOBALS['auth_port'];

    $strConn = "host=$auth_host port=$auth_port dbname=$auth_dbase user=$auth_user password=$auth_pass";

    $db = pg_connect($strConn) or die ("Error connection: " .pg_last_error());
	
	$player = pg_escape_string($_POST['name']);
	
	$sql = pg_query("SELECT item FROM attributes WHERE player = '$player'");
	
	$row = pg_fetch_row($sql);

 	echo $row[0];
	pg_close($db);
?> 