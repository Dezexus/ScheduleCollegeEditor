   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   TextBox.Static.Border  �� Color  TextBox.MouseOver.Border  TextBox.Focus.Border  TextBoxBase �� 
TargetType �� Property �� Value-   'System.Windows.Input.KeyboardNavigation �� 
TargetType ��Name J� SnapsToDevicePixels �� CornerRadius J� Effect3  -System.Windows.Media.Effects.DropShadowEffect	   Opacity
   ShadowDepth   
BlurRadius ��Name ��Name
 � Tag �� Triggers P� Property P� Value �� 
TargetName ]� 
Conditions �� Property �� Value N� EnterActions �� 
TargetName g� Duration �� TargetProperty	 �� To N� ExitActions �� Triggers�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %-	  &	        &	 !     &	 D     &	 g     7   7  �� 5      $  #d9d9d9��64   �� 5      $  	#FF7EB4EA��67   �� 5      $  	#FF569DE5��63   �� 5	      " ��6    .�5
   
   ��  � �� �#FFFFFF6'   �� 5   
    � ��8   6(   �� 5   
    � ��
 �   �6'   �� 5   
    � ��$	 1.3q�6,   �� 5   
    �@  TabNavigation$
 None=�6=   �� 5   
    � ��$ Center=�67   �� 5   
    � ��$ Center=�65   �� 5   
    � ��$ 16(�6%   �� 5   
    �@�FocusVisualStyle 6-   Z� �� 5   
    �@J�	AllowDrop . 6&   �� 5   
    �@��PanningMode$ VerticalFirst=�65   �� 5   
    �@��IsFlicksEnabled .  63   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      � .+�5      �� $ border��6!   #��z��61   #��z��6]   #��z��6�    . $ 5��6�    5        5   "   $		 0.1Q�63   $
 2Q�6A   $ 2Q�6Q   5      � 5      .+�5       �� $ PART_ContentHost��6+   |�.  $��Hidden=�6W   $��Hidden=�6~   �� 5!      $ 	WaterMark��6$   #��z 67   $y�Hidden=�6W   $��Center=�6k   $��Center=�6�   ���#5252526�   5"      5#       5%      P� 5&       � {� .  . �5'      �� $ border��68    �@J�Opacity$
 0.56Q�6L   5(      P� 5)       � ��$  ��62   . �5*      �� $ 	WaterMark��6%    � y�$ Visible=�6R   5+      P� 5,       �@J�IsKeyboardFocused . . �5-      �� $ border��6<   8   � ��6P   �� 5.      $ 	WaterMark��6%    � y�$ Hidden=�6R   �� 5/       � ��$ 2q�6@   50      ]� 52       53      �� 54   "    �@J�	IsFocused .  6A   �� 55   "    �@J�IsMouseOver .  6C    57      �� 58   "   .��59   &   �� .��5:   *   �� $ border��69   $ 0:0:0.5D�6X   $ BorderBrush.Color�5;   1   $ #d9d9d9X�6_   5<   +   5=   '   5>   #    5@      �� 5A   "   .��5B   &   �� .��5C   *   �� $ border��69   $ 0:0:0.5D�6X   $ BorderBrush.Color�5D   1   $ 	#FF569DE5X�6_   5E   +   5F   '   5G   #   5I      5L      5N       5O   
   ]� 5P       5Q      �� 5R      + �@��#IsInactiveSelectionHighlightEnabled . 6O   �� 5S       �@��IsSelectionActive .  6=   ./�5U      ��  �@��SelectionBrush# � /�63   5V      5X      5Z      