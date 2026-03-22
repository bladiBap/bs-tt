import 'package:flutter/material.dart';
import 'package:sol_test_mobile/core/router/app_router.dart';
import 'package:sol_test_mobile/features/service_locator.dart' as di;

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await di.init();
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Sol Test Mobile',
      theme: ThemeData(
        useMaterial3: true,
        colorSchemeSeed: Colors.blue,
        appBarTheme: const AppBarTheme(
          centerTitle: true,
          elevation: 2,
        ),
      ),
      initialRoute: AppRouter.productList,
      onGenerateRoute: AppRouter.generateRoute,
    );
  }
}
