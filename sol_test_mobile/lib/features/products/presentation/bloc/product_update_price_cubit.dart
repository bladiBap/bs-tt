import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:sol_test_mobile/features/products/domain/usecases/update_price_product.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/state/product_update_price_state.dart';

class ProducUpdatePriceCubit extends Cubit<UpdatePriceProductState> {
    final UpdatePriceProduct updatePriceProduct;

    ProducUpdatePriceCubit({required this.updatePriceProduct}) : super(UpdatePriceProductInitial());

    Future<void> updatePrice({required String productId, required double newPrice}) async {
        emit(UpdatePriceProductLoading());
        try {
            await updatePriceProduct(id: productId, price: newPrice);
            emit(UpdatePriceProductSuccess());
        } catch (e) {
            emit(UpdatePriceProductError("Can not update product price"));
        }
    }
}