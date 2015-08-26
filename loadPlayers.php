<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
	$auth_dbase = $GLOBALS['auth_dbase'];
    $auth_port = $GLOBALS['auth_port'];

    $strConn = "host=$auth_host port=$auth_port dbname=$auth_dbase user=$auth_user password=$auth_pass";
	
    $db = pg_connect($strConn) or die ("Error connection: " .pg_last_error());
	
	$account = pg_escape_string($_POST['account']);
	$player_query = pg_query("SELECT * FROM player WHERE account = '$account'");

	$rows = array();
	while($row = pg_fetch_array($player_query)){
		$rows[] = $row['name'].','.$row['custom'].','.$row['level'];
	}

 	echo implode("/",$rows);
	pg_close($db);
?> 