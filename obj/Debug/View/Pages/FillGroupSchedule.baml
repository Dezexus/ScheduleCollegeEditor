   M S B A M L   `   `   `  ����P  LScheduleCollegeEditor, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ScheduleCollegeEditor.ViewScheduleCollegeEditor.View  5      Q-clr-namespace:ScheduleCollegeEditor.ViewModelScheduleCollegeEditor.ViewModel  5      2    ,ScheduleCollegeEditor.View.FillGroupSchedule   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:ScheduleCollegeEditor.View     5      C	viewmodel-clr-namespace:ScheduleCollegeEditor.ViewModel     5      $��Stretch=�5	      $��Stretch=�5
        � DataContext  5      @  :ScheduleCollegeEditor.ViewModel.FillGroupScheduleViewModel  5   
    � 	Resources 5         BoolToVisConverter��%   &	        �� 5   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��330 �6   �� 5      $��* �6   $	��400��6)   .+�5    
   �� -   5         ��Name$
 test��5       $��10,-5q�6    �� CornerRadius$ 10��6/     Panel.Static.Background#���  6A   $��1q�6x     Border.Static.Brush#���  6�   $��3��6�    J� Effect 5!      3  -System.Windows.Media.Effects.DropShadowEffect  5"         Opacity$	 0.1Q�6#      ShadowDepth$ 3Q�61   	   
BlurRadius$	 2Q�6A   ._�5$      �  5%      �� 5&      $��40 �6'   �� 5'      $��* �6'   �� 5(      $��40 �6'   .+�5*      �� $��1��6   $��35��6-   $��Center=�69   
 �� Orientation$
 
Horizontal=�6V   .+�5+      �� $
��10,0q�5,      $	��200��5-      $��2��5.        ComboBoxBase#���  5/        
ItemsStyle#���  50       �� 
IsEditable . ��51      �� *
GroupsList+ �� SelectedItem 53      �� *SelectedGroup+ J� InputBindings 54      �� 55      
 �� Key$ Enter��6)    �� Command 65   �� *FillScheduleCommand+ �� CommandParameter 6]   �� *SelectedGroup+57      �� 58      $��Обновить��6   $	��150��60   $
��10,0q�6<     
ButtonBase#���  6J   ��6o   �� *FillScheduleCommand+��6�   �� *SelectedGroup+�� 59      $��Сохранить��6   $	��150��61   $
��10,0q�6=   #���  6K   ��6p   �� *SaveScheduleCommand+�� 5:      $��На практике��6   $	��150��63   $��10,0,0,0q�6?   #���  6Q   ��6v   �� *!ReplacePairsWithPracticeCommand+5;       � 5<      $��2��6�   ��6   �� *LoadAnimation+y�6:   ��  �� Path$ LoadAnimation� �� 	Converter# [  6�   5=      5>      � 5@   
   $��2��6   $��1��6   $��Center=�6,   $
��0,20q�6I    5A      �� 5B      $
��auto �6    �� 5C      $
��auto �6    �� 5D      $��* �6    .+�5G      �� $	��200��6R   $��1��6^   
 � Tag$ № Смены��6k     TextBoxBase#���  6y   $��Center=�6�   $��5q�6�   ��6   �� *Shift+{�6-   �� *ShiftBoxEnable+6�   �� 5H      $��2��6   .��5I      �� ��6   �� *WeekDayScheduleEditorList+��5J      �� 5K      .R�5L      � $��Center=�6(   5M      5O      5P      5Q      � 5S   
   $��1��6   $��10q�6   $��Stretch=�6(   $��Stretch=�6D   .+�5T      �� $��Stretch=�6   $��15(�62     ListBoxBase#���  6@   #���  6f     ItemsStyleOnlyRead#���  5U   N   ��6   �� * InfoIntersectionLessonCardList+6N   �� 5V       	 
IconButton#��[	 6    
 Tip#��[
 69   $��Right=�6X   $��Bottom=�6t   $��5q�6�    � ToolTip$� �Список занятий с которыми произошло пересечение с другой группой по кабинету��5W      5X      5Z      5\      