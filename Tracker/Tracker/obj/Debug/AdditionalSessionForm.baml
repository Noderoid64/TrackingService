   M S B A M L   `   `   `  ����B  >Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TrackerTracker  5   	   #    Tracker.AdditionalSessionForm   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Tracker     5   	     � Title$  AdditionalSessionForm��5   	   $	��110��6'   $	��360��64    � AllowsTransparency .  � WindowStyle$
 None=�6Z   ��6m   Z�  � 	Resources 5	         FocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 ButtonStyle1	 �� 
TargetType
 �� 
TargetType ��Name �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName��%  &	        &	 �     &	 �     &	 �     &	     &	 4    &	 U    &	 x    &	 �    &		 �    &	
 �    7   7  7  7  7  7 	 �� 5
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
 ��6&   .R�5(      �� $ border��6!   $ 5��61   #��z��6B   #��z��6n   #��z��6�    . ._�5)      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5*       5+      P� 5,       �@��IsDefaulted . . �5-   "   �� $ border��6@   # � ��6T    � ��5.      P� 5/       � {� .  . �50   "   �� $ border��6?   8   � ��6S   �� 51   "   $ border��6@   8   � ��6T   �� 52   "   $ contentPresenter��6K   8   � ��6i   53      55      57      58      59      .��5;      �� ���#303038$ 10��6"   ._�5=   
   ��  �� Orientation$ Vertical=�6   .+�5>      � $��10q�6   .+�5?      �� $
��Wrap=�6    �� TextAlignment$ Center=�60   $	��300��6G   
�������vtВаша смена закончилась. Вы хотите продлить смену ещё на 10 минут?5A      5B      �� 5C      $ 
Horizontal=�6   .+�5D      �� -   5         ��Name$ ButtonOk��5D      $��0q�6B   ���#1E8449
�������$��22(�6~   $��	20,0,10,0q�6�   $��50��6�   $	��100��6�   #��� 
 6�   .��6�   Да6�   �� 5E      -   5        $ ButtonNo��5E      $��0q�6B   ���#1E8449
�������$��22(�6~   $��50��6�   $	��100��6�   #��� 
 6�   .��6�   Нет6�   �� 5F      -   5         ��Name$ 
ClockLabel��5F      
�������$��40,0,0,0q�6C   $��Center=�6U   $��32(�6p   606�   5G      5H      5J      5K      