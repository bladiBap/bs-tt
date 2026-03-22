import 'package:get_it/get_it.dart';
import 'package:dio/dio.dart';
import 'package:sol_test_mobile/core/network/dio_client.dart';
import 'package:sol_test_mobile/features/products/data/repositories/product_repository.dart';
import 'package:sol_test_mobile/features/products/domain/repositories/iproduct_repository.dart';
import 'package:sol_test_mobile/features/products/domain/usecases/create_product.dart';
import 'package:sol_test_mobile/features/products/domain/usecases/get_product_by_id.dart';
import 'package:sol_test_mobile/features/products/domain/usecases/get_products_by_filter.dart';
import 'package:sol_test_mobile/features/products/domain/usecases/update_price_product.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/product_create_cubit.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/product_detail_cubit.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/product_list_cubit.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/product_update_price_cubit.dart';

final sl = GetIt.instance;

Future<void> init() async {
  sl.registerLazySingleton<Dio>(() => DioClient().instance);

  sl.registerLazySingleton<IProductRepository>(
    () => ProductRepositoryImpl(sl<Dio>()),
  );

  sl.registerLazySingleton(() => GetProductById(sl()));
  sl.registerLazySingleton(() => GetProductsByFilter(sl()));
  sl.registerLazySingleton(() => CreateProduct(sl()));
  sl.registerLazySingleton(() => UpdatePriceProduct(sl()));

  sl.registerFactory(() => ProductListCubit(
    getProductsByFilters: sl()
  ));

  sl.registerFactory(() => CreateProductCubit(
    createProduct: sl(),
  ));

  sl.registerFactory(() => ProductDetailCubit(
    getProductById: sl(),
  ));

  sl.registerFactory(() => ProducUpdatePriceCubit(
    updatePriceProduct: sl(),
  ));
}