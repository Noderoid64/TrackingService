   M S B A M L   `   `   `  ����B  >Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TrackerTracker  5   	       Tracker.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Tracker     5   	     � Title$  
MainWindow��5   	   $	��110��6   $	��360��6)    � AllowsTransparency .  � WindowStyle$
 None=�6O   
������  � 	Resources 5	         FocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 ButtonStyle1	 �� 
TargetType
 �� 
TargetType ��Name �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName��%E  &	        &	 �     &	 �     &	 �     &	     &	 4    &	 U    &	 x    &	 �    &		 �    &	
 �    7   7  7  7  7  7  7  7  7  7 	 �� 5
   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$    . #��� ��6J   $��1��6�   $	 1 2R�6�   5      5      5      �� 5   
   $ 	#FFDDDDDD��6;   �� 5   
   $ 	#FF707070��67   �� 5   
   $ #196F3D��6>   �� 5   
   $ 	#FF3C7FB1��6:   �� 5   
   $ #145A32��6<   �� 5   
   $ 	#FF2C628B��68   �� 5   
   $ 	#FFF4F4F4��6=   �� 5   
   $ 	#FFADB2B5��69   �� 5   
   $ 	#FF838383��6=   �� 5   
   "	 ��6%   .�5      ��  �@�FocusVisualStyle8   61   �� 5       � ��8  6+   �� 5       � ��8  6,   �� 5        � ��# � ��6+   �� 5!       � ��$ 1q�60   �� 5"       � ��$ Center=�6;   �� 5#       � ��$ Center=�69   �� 5$       � ��$ 1q�6(   �� 5%       � �� 5&      �� 5'      "
 ��6&   .R�5(      �� $ border��62   $ 5��6!   #��z��6B   #��z��6n   #��z��6�    . ._�5)      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5*       5+      P� 5,       �@��IsDefaulted . . �5-   "   �� $ border��6@   # � ��6T    � ��5.      P� 5/       �@J�IsMouseOver . . �50   "   �� $ border��6?   8   � ��6S   �� 51   "   $ border��6@   8   � ��6T   52      P� 53       � �� . . �54   "   �� $ border��6?   8   � ��6S   �� 55   "   $ border��6@   8   � ��6T   56      P� 57       � {� .  . �58   "   �� $ border��6?   8   � ��6S   �� 59   "   $ border��6@   8   � ��6T   �� 5:   "   $ contentPresenter��6K   8 	  � ��6i   5;      5=      5?      5@      5A      .��5C   
   �� ���#303038$ 10��6&   ._�5D      �� $��Center=�6   $	��110��64    �� Orientation$ Vertical=�6A   .+�5E      �� $ 5��6   ���#3a4149$��40��6?   $	��320��6K   $��10q�6W   ._�5F      �� $ 
Horizontal=�6!   .+�5G      �� $ 5��6!   $��40��62   ���#363b41._�5H      � .+�5I   "   �� $��5q�6(   $��/Resources/Icons/Men.png��63   6W   5J      5K      �� 5M      -   5         ��Name$ 
InputField��5M   "   
�������$��16(�6I   $��0q�6W   
������ $��10q�6�   $	��250��6�   $��Century Gothic)�6�   6�   5N      5O      �� 5R      -   5        $��0q�5R      $��40��6)   $	��320��65   $��0q�6A   $��28(�6L   ���#1E8449
�������#��� 
 6�   $��Century Gothic)�6�   .��6�   	Sing in6�   5T      5U      5X      