   M S B A M L   `   `   `  ���� H  DSmartCubeDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:SmartCubeDemoSmartCubeDemo      SmartCubeDemo.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:SmartCubeDemo       � Title$  SmartCube Demo��$
��Auto��$
��Auto��.���  � RowDefinitions �� $��20 ��� $	��110 ��� $	��110 ��� $
��243* � � ColumnDefinitions �� $	��150 ��� $
��238* ��� $	��140 �.+��� -   $��	cmdScroll��$��1��$��2��$��3��$
��Auto��$
��Auto��$��Hidden=����	#FFDEE0DE.���� -   $��commandLine��$
��5000��$��Stretch=�$��Bottom=�$��1��$��2��$��3������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ Black�� � Offset$ 0Q�� $ 	#FF02214D��$ 1Q�� $ 	#FF0C2547��$ 0.039Q�� $ 	#FF2063C1��$ 0.955Q��� $��Res/amc.png��$��0��$��1���� -   $��cubeElements��$��1��$��Stretch=�$��Stretch=�$��1��$��1�����	#FFBCD8EC$	��Top=����� .R�� $	��Top=����� * (FrameworkElement.ActualWidth)+���� .R���  �� Orientation$ Vertical=�$	��Top=�.+��� $
��Bold"�$��14(�$��Center=�����  �� Path$
 Name��� ���� $ Image��� $��2��$��2��$��0��$��2��$
��Auto��$
��Auto��$��Visible=����	#FFDEE0DE.���� -   $��mapView��$
��3000��$
��3000��.+��� -   $��arrowChoiser��	 K� ZIndex$		 100��$	��120��$	��120�����	#3F13FB1D.+��� -   $��arrowUp��$��Res/choise-arrow.png��
 �� Left$
 39��$��42��$��42���� -   $��	arrowLeft��$��Res/choise-arrow.png��
 �� Top$ 39��$��42��$��42�� J� RenderTransformOrigin$ 0.5,0.5&�z�V� .~��� �� ��  �� Angle$	 -90Q�T� �� -   $��
arrowRight��$��Res/choise-arrow.png��$ 39��$
 79��$��42��$��42��$ 0.5,0.5&�z�V� .~��� �� �� $ 90Q�T� �� -	   $��	arrowDown��$��Res/choise-arrow.png��$
 39��$ 79��$��42��$��42��$ 0.5,0.5&�z�V� .~��� �� �� $	 180Q�T� �� -
   $��arrowFin��$��30��$��30��$
 45��$ 44��$��Res/Fin.png��o� $��3��$��20��$	��Top=�$��Stretch=�.@�m� $��20��$��PROJECTS��.@�m� -   $��20��$��Save...��m� -   $��Rescent��$��20��$��Rescent��m� $��20��$��CLEAR��.@�m� -   $��20��$��	Reset all��m� -   $��20��$��Clear commands only��m� -   $��miRun��$��20��$	��RUN��