   M S B A M L   `   `   `  ���� P  LScheduleCollegeEditor, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ScheduleCollegeEditor.ViewScheduleCollegeEditor.View  Q-clr-namespace:ScheduleCollegeEditor.ViewModelScheduleCollegeEditor.ViewModel  &     ScheduleCollegeEditor.View.Times   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:ScheduleCollegeEditor.View     C	viewModel-clr-namespace:ScheduleCollegeEditor.ViewModel     -     N�Name$  Window��$��Stretch=�$��Stretch=�$	��600��$	��300��$��10q� � DataContext 4  .ScheduleCollegeEditor.ViewModel.TimesViewModel   � 	Resources    BoolToVisConverter��%   &	        �� .��� $��Center=� � RowDefinitions �� $��70 ��� $
��auto ��� $
��auto �.+�� $
��10,0q�$��35��.+��� $,��&Сохранить как шаблон��$	��200��  
ButtonBase#���  $
��Left=����� *$"CreateTemplateScheduleCallsCommand+����  �� ElementName$ Window���� $��Сохранить��$	��200��#���  ���� *SaveCallsScheduleCommand+�� $)��#Шаблоны расписания��$	��200��#���  $��Right=����� *(&OpenTemplateScheduleCallsWindowCommand+ � $��Right=����� *LoadAnimation+y���  �� Path$ LoadAnimation� �� 	Converter# [  �� $��2��$
��Auto=�$
��Auto=�.���  �� $��30 ��� $
��auto ��� $��40 � � ColumnDefinitions �� $	��250 ��� $	��250 ��� $	��250 ��� $	��250 ��� $	��250 ��� $	��250 �.+��� ���#FFDC14 �� CornerRadius$ 5��$��25��$	��5,0q�._��� $��Понедельник��$��Center=�$��Center=�$��14(��� $��1�����#FFDC14$ 5��$��25��$	��5,0q�._��� $��Вторник��$��Center=�$��Center=�$��14(��� $��2�����#FFDC14$ 5��$��25��$	��5,0q�._��� $��
Среда��$��Center=�$��Center=�$��14(��� $��3�����#FFDC14$ 5��$��25��$	��5,0q�._��� $��Четверг��$��Center=�$��Center=�$��14(��� $��4�����#FFDC14$ 5��$��25��$	��5,0q�._��� $��Пятница��$��Center=�$��Center=�$��14(��� $��5�����#FFDC14$ 5��$��25��$	��5,0q�._��� $��Суббота��$��Center=�$��Center=�$��14(��� $��0��$��1��$��5q����� *MondayTimeSchedule+���� .R��� �� $��1��$��1��$��5q����� *TuesdayTimeSchedule+���� .R��� �� $��2��$��1��$��5q����� *WednesdayTimeSchedule+���� .R��� �� $��3��$��1��$��5q����� *ThursdayTimeSchedule+���� .R��� �� $��4��$��1��$��5q����� *FridayTimeSchedule+���� .R��� �� $��5��$��1��$��5q����� *SaturdayTimeSchedule+���� .R��� �� -   	 ��Name$	 MoBtn��$��Добавить��$��0��$��2��$	��5,5q�#���  

 � Tag$

 Пн������ *AddTimeLessonCommand+���� $ MoBtn��$	 Tag��� -   $	 TuBtn��$��Добавить��$��1��$��2��$	��5,5q�#���  $

 Вт������ *AddTimeLessonCommand+���� $ TuBtn��$	 Tag��� -   $	 WeBtn��$��Добавить��$��2��$��2��$	��5,5q�#���  $

 Ср������ *AddTimeLessonCommand+���� $ WeBtn��$	 Tag��� -   $	 ThBtn��$��Добавить��$��3��$��2��$	��5,5q�#���  $

 Чт������ *AddTimeLessonCommand+���� $ ThBtn��$	 Tag��� -   $	 FrBtn��$��Добавить��$��4��$��2��$	��5,5q�#���  $

 Пт������ *AddTimeLessonCommand+���� $ FrBtn��$	 Tag��� -   $	 SaBtn��$��Добавить��$��5��$��2��$	��5,5q�#���  $

 Сб������ *AddTimeLessonCommand+���� $ SaBtn��$	 Tag�