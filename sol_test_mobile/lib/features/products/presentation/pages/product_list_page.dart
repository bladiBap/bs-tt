import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/product_list_cubit.dart';
import 'package:sol_test_mobile/features/products/presentation/widgets/product_list_view.dart';
import 'package:sol_test_mobile/features/service_locator.dart';

class ProductListPage extends StatelessWidget {
  const ProductListPage({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (_) => sl<ProductListCubit>()..fetchProducts(),
      child: const ProductListView(),
    );
  }
}