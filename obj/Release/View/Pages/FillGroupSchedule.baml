   M S B A M L   `   `   `  ���� P  LScheduleCollegeEditor, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ScheduleCollegeEditor.ViewScheduleCollegeEditor.View  Q-clr-namespace:ScheduleCollegeEditor.ViewModelScheduleCollegeEditor.ViewModel  2    ,ScheduleCollegeEditor.View.FillGroupSchedule   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:ScheduleCollegeEditor.View     C	viewmodel-clr-namespace:ScheduleCollegeEditor.ViewModel     $��Stretch=�$��Stretch=�  � DataContext  @  :ScheduleCollegeEditor.ViewModel.FillGroupScheduleViewModel   � 	Resources    BoolToVisConverter��%   &	        �� .���  � RowDefinitions �� $��40 ��� $��* � � ColumnDefinitions �� $	��330 ��� $��* �$	��400��.+��� -    ��Name$
 test��$��10,-5q� �� CornerRadius$ 10��  Panel.Static.Background#���  $��1q�  Border.Static.Brush#���  $��3�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Opacity$	 0.1Q�   ShadowDepth$ 3Q�	   
BlurRadius$	 2Q�._��  �� $��40 ��� $��* ��� $��40 �.+��� $��1��$��35��$��Center=�
 �� Orientation$
 
Horizontal=�.+��� $
��10,0q�$	��200��$��2��  ComboBoxBase#���    
ItemsStyle#���   �� 
IsEditable . ���� *
GroupsList+ �� SelectedItem �� *SelectedGroup+ J� InputBindings �� 
 �� Key$ Enter�� �� Command �� *FillScheduleCommand+ �� CommandParameter �� *SelectedGroup+�� $��Обновить��$	��150��$
��10,0q�  
ButtonBase#���  ���� *FillScheduleCommand+���� *SelectedGroup+�� $��Сохранить��$	��150��$
��10,0q�#���  ���� *SaveScheduleCommand+�� $��На практике��$	��150��$��10,0,0,0q�#���  ���� *!ReplacePairsWithPracticeCommand+ � $��2������ *LoadAnimation+y���  �� Path$ LoadAnimation� �� 	Converter# [  � $��2��$��1��$��Center=�$
��0,20q� �� $
��auto ��� $
��auto ��� $��* �.+��� $	��200��$��1��
 � Tag$ № Смены��  TextBoxBase#���  $��Center=�$��5q����� *Shift+{��� *ShiftBoxEnable+�� $��2��.���� ���� *WeekDayScheduleEditorList+���� .R�� $��Center=�� $��1��$��10q�$��Stretch=�$��Stretch=�.+��� $��Stretch=�$��15(�  ListBoxBase#���  #���    ItemsStyleOnlyRead#���  ���� * InfoIntersectionLessonCardList+��  	 
IconButton#��[	  
 Tip#��[
 $��Right=�$��Bottom=�$��5q� � ToolTip$� �Список занятий с которыми произошло пересечение с другой группой по кабинету��