   M S B A M L   `   `   `  ????A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ClientClient  5   	       Client.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Client     5   	     ? Title$  Client??5   	   $	??620??6   $	??800??6%   .??5	      ? $??15q?6    ? RowDefinitions 5
   
   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   .+?5   
   ?? $??0??6   .??5      ?? .?6   Files Available to Download60   5      ?? 5   
   -   5         ??Name$ filenamesListBox??5   !   $??1??6   $??10q?6;   $	??200??6G   6V   ?? 5   
   -   5         ??Name$ downloadButton??5       $??2??6   $??10, 10, 420, 10q?68   $??30??6Q   .??6z   
Download6?   ?? 5   
   -   5        $ 
viewButton??5       $??2??6   $??420, 10, 10, 10q?64   $??30??6M   .??6r   View6x   ?? 5   
   $??3??6   .??5      ?? .?6   File Viewer6    5      ?? 5   
   -   5         ??Name$ CurrentFileName??5       ?? 
IsReadOnly$
 True??6+   $??4??6=   $??10q?6L   .??6X   Current File: None6l   ?? 5   
   -   5        $ ViewBox??5   !   $??5??6   $
 True??62   $??10q?6D   $	??180??6P   .??6]   '%Please select a file and click "view"6?   5      5      