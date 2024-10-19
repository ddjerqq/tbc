# Generated by the gRPC Python protocol compiler plugin. DO NOT EDIT!
"""Client and server classes corresponding to protobuf-defined services."""
import grpc
import warnings

import ai_advisor_pb2 as ai__advisor__pb2

GRPC_GENERATED_VERSION = '1.66.2'
GRPC_VERSION = grpc.__version__
_version_not_supported = False

try:
    from grpc._utilities import first_version_is_lower
    _version_not_supported = first_version_is_lower(GRPC_VERSION, GRPC_GENERATED_VERSION)
except ImportError:
    _version_not_supported = True

if _version_not_supported:
    raise RuntimeError(
        f'The grpc package installed is at version {GRPC_VERSION},'
        + f' but the generated code in ai_advisor_pb2_grpc.py depends on'
        + f' grpcio>={GRPC_GENERATED_VERSION}.'
        + f' Please upgrade your grpc module to grpcio>={GRPC_GENERATED_VERSION}'
        + f' or downgrade your generated code using grpcio-tools<={GRPC_VERSION}.'
    )


class AiAdvisorStub(object):
    """Missing associated documentation comment in .proto file."""

    def __init__(self, channel):
        """Constructor.

        Args:
            channel: A grpc.Channel.
        """
        self.GetFinancialAdvice = channel.unary_unary(
                '/AiAdvisor.AiAdvisor/GetFinancialAdvice',
                request_serializer=ai__advisor__pb2.GetFinancialAdviceForAccountRequest.SerializeToString,
                response_deserializer=ai__advisor__pb2.FinancialAdviceResponse.FromString,
                _registered_method=True)


class AiAdvisorServicer(object):
    """Missing associated documentation comment in .proto file."""

    def GetFinancialAdvice(self, request, context):
        """Missing associated documentation comment in .proto file."""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')


def add_AiAdvisorServicer_to_server(servicer, server):
    rpc_method_handlers = {
            'GetFinancialAdvice': grpc.unary_unary_rpc_method_handler(
                    servicer.GetFinancialAdvice,
                    request_deserializer=ai__advisor__pb2.GetFinancialAdviceForAccountRequest.FromString,
                    response_serializer=ai__advisor__pb2.FinancialAdviceResponse.SerializeToString,
            ),
    }
    generic_handler = grpc.method_handlers_generic_handler(
            'AiAdvisor.AiAdvisor', rpc_method_handlers)
    server.add_generic_rpc_handlers((generic_handler,))
    server.add_registered_method_handlers('AiAdvisor.AiAdvisor', rpc_method_handlers)


 # This class is part of an EXPERIMENTAL API.
class AiAdvisor(object):
    """Missing associated documentation comment in .proto file."""

    @staticmethod
    def GetFinancialAdvice(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            insecure=False,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(
            request,
            target,
            '/AiAdvisor.AiAdvisor/GetFinancialAdvice',
            ai__advisor__pb2.GetFinancialAdviceForAccountRequest.SerializeToString,
            ai__advisor__pb2.FinancialAdviceResponse.FromString,
            options,
            channel_credentials,
            insecure,
            call_credentials,
            compression,
            wait_for_ready,
            timeout,
            metadata,
            _registered_method=True)
