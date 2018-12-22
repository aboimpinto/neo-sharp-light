## Builder stage
# use official Microsoft dotnet container
FROM microsoft/dotnet:latest as builder

# make builds faster. Caching packages on a temporary build machine is a waste of time
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# opt-out telemetry
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1

# create folders structure and clone neo-sharp repo
RUN mkdir /repo && \
    cd /repo && \
    git clone --recursive https://github.com/aboimpinto/neo-sharp-light.git && \
    cd /repo/neo-sharp-light/sources/Extraction/NeoSharpLight.RPC.BlockchainExtraction && \
    dotnet publish --configuration Release --output /home/neo-sharp-light

## Runtime stage
# use official Microsoft dotnet container
FROM microsoft/dotnet:2.1-runtime as runtime

# make builds faster. Caching packages on a temporary build machine is a waste of time
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# opt-out telemetry
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1

# create folder for application
RUN mkdir /home/neo-sharp

# copy app from builder stage
COPY --from=builder /home/neo-sharp-light/* /home/neo-sharp-light/

# workdir
WORKDIR /home/neo-sharp-light

# default first command to run starting app
CMD ["bash", "-c",  "/usr/bin/dotnet NeoSharpLight.RPC.BlockchainExtraction.dll $START_BLOCK $END_BLOCK $SEED"]